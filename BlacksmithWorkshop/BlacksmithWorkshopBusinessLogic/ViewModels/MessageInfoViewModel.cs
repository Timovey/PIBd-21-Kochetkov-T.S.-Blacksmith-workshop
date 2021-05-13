using System;
using System.Runtime.Serialization;
using BlacksmithWorkshopBusinessLogic.Attributes;

namespace BlacksmithWorkshopBusinessLogic.ViewModels
{
    /// <summary>
    /// Сообщения, приходящие на почту
    /// </summary>
    [DataContract]
    public class MessageInfoViewModel
    {
        [DataMember]
        public string MessageId { get; set; }
        [DataMember]
        [Column(title: "Отправитель", width: 100)]
        public string SenderName { get; set; }
        [Column(title: "Дата письма", width: 50)]
        [DataMember]
        public DateTime DateDelivery { get; set; }
        [Column(title: "Заголовок", width: 150)]
        [DataMember]
        public string Subject { get; set; }
        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string Body { get; set; }
    }

}
