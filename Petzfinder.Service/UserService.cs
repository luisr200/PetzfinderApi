using Petzfinder.Data;
using Petzfinder.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Petzfinder.Service
{
    public class UserService
    {
        public async Task<User> GetUserByKey(string key)
        {
            UserRepository _repo = new UserRepository();
            return await _repo.GetUserByKey(key);

        }

        public async Task PutUser(User key)
        {
            UserRepository _repo = new UserRepository();
            await _repo.PutUser(key);

        }
    }
}
