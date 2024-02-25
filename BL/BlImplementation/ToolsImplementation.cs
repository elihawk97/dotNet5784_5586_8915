using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BO.Enums;

namespace BL.BlImplementation
{
    /// <summary>
    /// Implements project-related tools and utilities.
    /// Interacts with the DAL for project data access and manipulation.
    /// </summary>
    internal class ToolsImplementation : BlApi.ITools
    {
        /// <summary>
        /// DAL instance for data access.
        /// </summary>
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

        /// <summary>
        /// Gets the project's start date.
        /// </summary>
        /// <returns>The project's start date, or null if not set.</returns>
        public DateTime? getProjectStartDate()
        {
            return _dal.getProjectStartDate();
        }

        /// <summary>
        /// Sets the project's end date.
        /// </summary>
        /// <param name="endDate">The new end date to set.</param>
        public void SetProjectEndDate(DateTime? endDate)
        {
            _dal.SetProjectEndDate(endDate);
        }

        /// <summary>
        /// Sets the project's start date.
        /// </summary>
        /// <param name="startDate">The new start date to set.</param>
        public void SetProjectStartDate(DateTime? startDate)
        {
            _dal.SetProjectStartDate(startDate);
        }
    }
}
