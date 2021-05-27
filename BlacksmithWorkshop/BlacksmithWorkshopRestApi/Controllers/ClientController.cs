using BlacksmithWorkshopBusinessLogic.BindingModels;
using BlacksmithWorkshopBusinessLogic.BusinessLogic;
using BlacksmithWorkshopBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;


namespace BlacksmithWorkshopRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientLogic _logic;
        private readonly MailLogic _mailLogic;
        private readonly int _passwordMaxLength = 50;
        private readonly int _passwordMinLength = 5;

        public ClientController(ClientLogic logic, MailLogic mailLogic)
        {
            _logic = logic;
            _mailLogic = mailLogic;
        }
        [HttpGet]
        public ClientViewModel Login(string login, string password) => _logic.Read(new ClientBindingModel
        { Email = login, Password = password })?[0];
        [HttpGet]
        public List<MessageInfoViewModel> GetMessages(int clientId) =>
       _mailLogic.Read(new MessageInfoBindingModel { ClientId = clientId });
        [HttpPost]

        public void Register(ClientBindingModel model)
        {
            CheckData(model); 
            _logic.CreateOrUpdate(model);
        }
        [HttpPost]
        public void UpdateData(ClientBindingModel model)
        {
            CheckData(model);
            _logic.CreateOrUpdate(model);
        }
        private void CheckData(ClientBindingModel model)
        {
            //"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$"
            if (!Regex.IsMatch(model.Email, @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"+"@"+ @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$"))
            {
                throw new Exception("В качестве логина почта указана должна быть");
            }
            if (model.Password.Length > _passwordMaxLength || model.Password.Length <
           _passwordMinLength || !Regex.IsMatch(model.Password,
            @"^((\w+\d+\W+)|(\w+\W+\d+)|(\d+\w+\W+)|(\d+\W+\w+)|(\W+\w+\d+)|(\W+\d+\w+))[\w\d\W]*$"))
            {
                throw new Exception($"Пароль длиной от {_passwordMinLength} до { _passwordMaxLength } должен быть и из цифр, букв и небуквенных символов должен состоять");
            }
        }

        [HttpGet]
        public PageViewModel GetPage(int pageSize, int page, int ClientId)
        {
            return new PageViewModel
            {
                PageSize = pageSize
                ,
                PageNumber =
                page,
                TotalItems = _mailLogic.Count(),
                Messages =
                _mailLogic.GetMessagesPage(new MessageInfoBindingModel
                {
                    Page = page,
                    PageSize = pageSize,
                    ClientId = ClientId

                })
            };
        }
    }
}
