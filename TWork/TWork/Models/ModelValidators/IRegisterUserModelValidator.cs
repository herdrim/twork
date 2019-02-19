using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWork.Models.ViewModels;

namespace TWork.Models.ModelValidators
{
    public interface IRegisterUserModelValidator
    {
        Task<bool> ValidateRegisterUserModel(RegisterUserViewModel userModel, ModelStateDictionary ModelState);
    }
}
