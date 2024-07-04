using StoryTrails.Communication.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryTrails.Application.UseCases.Collections.interfaces
{
    public interface IUpdateCollectionUseCase
    {
        Task<bool> Execute(string id, CollectionJsonRequest requestBody, string userToken);
    }
}
