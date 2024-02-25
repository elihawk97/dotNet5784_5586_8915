using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BL.BlImplementation
{
    internal class ToolsImplementation : BlApi.ITools
    {
        private DalApi.IDal _dal = DalApi.Factory.Get;

        // Backing fields for the properties
        private ProjectStages _currentProjectStage;
        private User _currentUser;

        // Implementing properties from the ITools interface

        public ToolsImplementation()
        {
            // Set the default user to Admin
            _currentUser = User.Admin;
            // Set the default project stage to Planning
            _currentProjectStage = ProjectStages.Planning;
        }

        public ProjectStages CurrentProjectStage
        {
            get => _currentProjectStage;
            set => _currentProjectStage = value;
        }

        public User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }

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
