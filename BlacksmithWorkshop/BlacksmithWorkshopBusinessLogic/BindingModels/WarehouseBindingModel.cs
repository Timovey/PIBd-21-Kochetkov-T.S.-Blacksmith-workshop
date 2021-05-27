using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace BlacksmithWorkshopBusinessLogic.BindingModels
{

    /// <summary>
    /// склад для храниния компонент
    /// </summary>
    [DataContract]
    public class WarehouseBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public DateTime DateCreate { get; set; }
        [DataMember]
        public Dictionary<int, (string, int)> WarehouseComponents { get; set; }

    }
}
