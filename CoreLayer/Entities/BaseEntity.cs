using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class BaseEntity:IBaseEntity
    {
        //ctor oluşturarak yapmak performansı azaltır propertyere bağlı olarak yap
        //public BaseEntity()
        //{
        //    Id=Guid.NewGuid();//yeni guid ıd üretiyor 01 olarak int gibi artmıyor
        //    CreatedDate=DateTime.Now;
        //}

        public virtual Guid Id  { get; set; } =Guid.NewGuid();
        public virtual string CreatedBy { get; set; } = "Undefined";
        public virtual string? ModifiedBy { get; set; }
        public virtual string? DeletedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; } = DateTime.Now;
        public virtual DateTime? MoodifiedDate { get; set; }
        public virtual DateTime? DeletedDate { get; set; }
       public virtual bool IsDeleted { get; set; }  = false;

    }
}
