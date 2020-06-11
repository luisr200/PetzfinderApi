using Petzfinder.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Petzfinder.Data;
using System.Threading.Tasks;

namespace Petzfinder.Service
{
    public class TagService
    {
        public async Task<List<Tags>> GetUnprintedTags()
        {
            TagsRepository _repo = new TagsRepository();
            return await _repo.GetUnprintedTags();
        }

        public async Task InsertTags(List<Tags> tags)
        {
            TagsRepository _repo = new TagsRepository();
            foreach (var tag in tags)
            {
                await _repo.PutTag(tag);
            }

        }

        public async Task UpdateTags(List<Tags> tags)
        {
            TagsRepository _repo = new TagsRepository();
            foreach (var tag in tags)
            {
                await UpdateTag(tag);
            }

        }

        public async Task UpdateTag(Tags tag)
        {
            TagsRepository _repo = new TagsRepository();
            await _repo.PutTag(tag);

        }

        public async Task<Tags> GetTagById(string id)
        {
            TagsRepository _repo = new TagsRepository();
            return await _repo.GetTagById(id);

        }

        public async Task<List<Tags>> GetAllTags()
        {
            TagsRepository _repo = new TagsRepository();
            return await _repo.GetAllTags();

        }
    }
}
