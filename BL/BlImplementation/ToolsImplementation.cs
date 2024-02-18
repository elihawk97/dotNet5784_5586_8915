using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.BlImplementation
{
    internal class ToolsImplementation : BlApi.ITools
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;


        public DateTime? getProjectEndDate()
        {
            return _dal.getProjectEndDate();
        }

        public DateTime? getProjectStartDate()
        {
            return _dal.getProjectStartDate();
        }

        public void SetProjectEndDate(DateTime? endDate)
        {
            _dal.SetProjectEndDate(endDate);
        }

        public void SetProjectStartDate(DateTime? startDate)
        {
            _dal.SetProjectStartDate(startDate);
        }
    }
}
