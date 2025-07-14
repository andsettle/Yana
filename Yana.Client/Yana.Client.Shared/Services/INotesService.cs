using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yana.Client.Shared.Models;

namespace Yana.Client.Shared.Services
{
    public interface INotesService
    {
        Task<List<Note>> GetNotesAsync();
    }
}
