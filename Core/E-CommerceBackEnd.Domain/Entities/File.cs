using E_CommerceBackEnd.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceBackEnd.Domain.Entities
{
    public class File:BaseEntities
    {

        public string FileName { get; set; }

        public string Path { get; set; }

        [NotMapped]  //migrationda updatedate ekleme
        public override DateTime UpdatedDate { get => base.UpdatedDate; set => base.UpdatedDate = value; }


    }
}
