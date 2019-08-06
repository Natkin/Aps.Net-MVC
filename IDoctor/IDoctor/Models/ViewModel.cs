using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDoctor.Models
{
    public class ViewModel
    {
        public IEnumerable<IDoctor.ElQueue> Q { get; set; }
        public PagedList.IPagedList<IDoctor.Patient> Pats { get; set; }
    }
}
