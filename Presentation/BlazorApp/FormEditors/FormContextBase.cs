using AutoMapper;
using Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.FormEditors
{
    public abstract class FormContextBase<T> where T: BaseDto, new()
    {
        public virtual void Init()
        {
            Mapper.Map(DataItem, this);
        }

        public virtual void PrepareDataItem()
        {
            Mapper.Map(this, DataItem);
        }

        public IMapper Mapper { get; set; }
        public bool IsNewRow { get; set; }
        public T DataItem { get; set; } = new T();
        public Action StateHasChanged { get; set; }
    }
}
