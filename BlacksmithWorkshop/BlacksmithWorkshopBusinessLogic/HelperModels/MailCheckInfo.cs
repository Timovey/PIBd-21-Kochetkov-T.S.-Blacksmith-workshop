using BlacksmithWorkshopBusinessLogic.Interfaces;

namespace BlacksmithWorkshopBusinessLogic.HelperModels
{
    public class MailCheckInfo
    {
        public string PopHost { get; set; }
        public int PopPort { get; set; }
        public IMessageInfoStorage Storage { get; set; }
    }

}
