using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using CsvHelper;
using Newtonsoft.Json;

using PJE.WatchOrders.Data;
using PJE.WatchOrders.Models;

namespace PJE.WatchOrders
{
    public class Manager
    {
        private readonly Options _options;
        private readonly IRepository _repository;
        private readonly IInput _input;

        public Manager(Options options, IRepository repository, IInput input)
        {
            _options = options;
            _repository = repository;
            _input = input;
        }

        public void RunForever()
        {
            Console.WriteLine($"watching {_options.InputDirectory}");
            while (true)
            {
                var file = _input.GetNext();
                ProcessFileAsync(file).Wait();
            }
        }

        private async Task ProcessFileAsync(InputFile file)
        {
            Console.WriteLine($"new file detected at {file.FileName}");

            if (file.FileName.ToLowerInvariant().Contains("shipment"))
            {
                var parsed = JsonConvert.DeserializeObject<ShippingFile>(file.Contents);
                await _repository.AddShippingsAsync(parsed.Orders);
            }
            else if (file.FileName.ToLowerInvariant().Contains("items"))
            {
                var parsed = JsonConvert.DeserializeObject<ItemFile>(file.Contents);
                await _repository.AddItemsAsync(parsed.OrderItems);
            }
            else
            {
                var parsed = JsonConvert.DeserializeObject<OrderFile>(file.Contents);
                await _repository.AddOrdersAsync(parsed.Orders);
            }

            await SaveNewLinesAsync();
        }

        private async Task SaveNewLinesAsync()
        {
            var lines = await _repository.GetCompletedLinesAsync();
            await SaveOutputAsync(lines);
        }

        private async Task SaveOutputAsync(IEnumerable<OrderLine> orderLines)
        {
            foreach (var group in orderLines
                .GroupBy(ol => new {ol.Marketplace, ol.OrderReference}))
            {
                var directory = Path.Combine(
                    _options.OutputDirectory,
                    group.Key.Marketplace);

                Directory.CreateDirectory(directory);

                var outputPath = Path.Combine(
                    directory,
                    $"{group.Key.OrderReference}.csv");

                await Console.Out.WriteAsync($"writing to {outputPath} ...");

                using (var textWriter = new StreamWriter(outputPath))
                {
                    var writer = new CsvWriter(textWriter);
                    writer.WriteRecords(group);
                    await writer.FlushAsync();
                    await Console.Out.WriteLineAsync($"done");
                }
            }
        }
    }
}
