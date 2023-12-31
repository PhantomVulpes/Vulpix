﻿using Knox.Extensions;
using Newtonsoft.Json;
using Vulpix.Domain.Commanding;
using Vulpix.Domain.Repositories;

namespace Vulpix.Domain.Commands
{
    public record LegacyLoadFromFileCommandContext(string SaveDirectory, string FileName) : CommandContext();
    public class LegacyLoadFromFileCommand : ICommand<LegacyLoadFromFileCommandContext, ISaveFile>
    {
        public ISaveFile Execute(LegacyLoadFromFileCommandContext context)
        {
            try
            {
                var data = File.ReadAllText($"{context.SaveDirectory}\\{context.FileName}");
                var result = JsonConvert.DeserializeObject<SaveFile>(data).Wrap().UnwrapOrExchange(new())!;
                return result;
            }
            catch
            {
                return new SaveFile();
            }
        }
    }
}
