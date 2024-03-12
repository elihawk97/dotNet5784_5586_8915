using BlApi;
using BO;
using System;
using System.Collections.Generic;


namespace BlImplementation;

/// <summary>
/// Class implementing the IEngineer interface to manipulate the
/// Engineer list object of the Bl
/// </summary>
internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    /// <summary>
    /// Creates a new engineer record in the system.
    /// Validates the provided boItem for mandatory fields and data integrity.
    /// Throws exceptions for invalid data or duplicate engineer IDs.
    /// Returns the ID of the newly created engineer.
    /// </summary>
    /// <param name="boItem">The engineer data to be created.</param>
    /// <returns>The ID of the newly created engineer.</returns>
    public int Create(BO.Engineer boItem)
    {
        
        // Check for engineer's name (non-empty string)
        if (string.IsNullOrEmpty(boItem.Name))
        {
            throw new Exception("Engineer's name cannot be empty.");
        }

        if (boItem.Cost <= 0)
        {
            throw new Exception("Invalid cost. Must be a positive value.");
        }

        if (!IsValidEmail(boItem.Email))
        {
            throw new Exception("Invalid email address pattern.");
        }

        DO.Engineer doEngineer = new DO.Engineer(
            boItem.Id,
            boItem.Name,
            boItem.Email,
            (DO.Enums.ExperienceLevel)boItem.Level,
            boItem.Cost);

        try
        {

            int idEngineer = _dal.Engineer.Create(doEngineer);
            return idEngineer;
        }

        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BlInvalidTaskCreation(
            $"Engineer with ID={boItem.Id} already exists", ex
            );
        }
    }

    /// <summary>
    /// Deletes an engineer record with the specified ID.
    /// Throws an exception if the engineer with the given ID does not exist.
    /// </summary>
    /// <param name="id">The ID of the engineer to delete.</param>
    public void DeleteEngineer(int id)
    {
        try
        {
            _dal.Engineer.Read(x => x.Id == id);

            _dal.Engineer.Delete(id);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"There was an error deleting the Engineer with id={id}", ex);
        }
    }
    /// <summary>
    /// Retrieves all engineer records from the system.
    /// Optionally applies a provided filter function to the results.
    /// Maps retrieved DO.Engineer objects to BO.Engineer objects.
    /// Throws an exception if an error occurs while reading engineers.
    /// </summary>
    /// <param name="filter">Optional filter function to apply to the results.</param>
    /// <returns>An IEnumerable of BO.Engineer objects representing the retrieved engineers.</returns>

    public IEnumerable<BO.Engineer> ReadAll(Func<Engineer, bool> filter)
    {
        try
        {
            IEnumerable<DO.Engineer> doEngineers = _dal.Engineer.ReadAll();
            IEnumerable<BO.Engineer> BO_Engineers;
            if (filter != null)
            {
                // Return engineers that are not assigned to a task and that 
                // have the proper skill level for the task
                // will also return an engineer that finished his previous task
                BO_Engineers = doEngineers.Select(doEngineer => ConvertDOtoBO(doEngineer)).Where(filter).Where(boEngineer => (boEngineer.Task == null) ||(boEngineer.Task.ActualEndDate != null));
                return BO_Engineers;
            }
            else
            {
                BO_Engineers = doEngineers.Select(doEngineer => ConvertDOtoBO(doEngineer));  // Convert all fetched records
                return BO_Engineers; 

            }
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"Error occurred while reading engineers", ex);
        }
    }

    /// <summary>
    /// Retrieves an engineer record with the specified ID.
    /// Returns the engineer as a BO.Engineer object if found, otherwise returns null.
    /// Throws an exception if the engineer with the given ID does not exist.
    /// </summary>
    /// <param name="id">The ID of the engineer to retrieve.</param>
    /// <returns>The retrieved engineer as a BO.Engineer object, or null if not found.</returns>

    public Engineer? ReadEngineer(int id)
    {
            try
            {
                DO.Engineer engineer = _dal.Engineer.Read(x => x.Id == id);
                BO.Engineer boEngineer= ConvertDOtoBO(engineer);
                return boEngineer;
            }
            catch (DO.DalDoesNotExistException ex)
            {
                throw new BO.BlDoesNotExistException($"Engineer with ID={id} does not exist", ex);
            }

    }

    /// <summary>
    /// Updates an existing engineer record with the provided details.
    /// Validates the provided boItem for mandatory fields and data integrity.
    /// Throws an exception if the engineer with the given ID does not exist.
    /// </summary>
    /// <param name="boItem">The updated engineer data.</param>
    public void UpdateEngineer(int id, Engineer boEngineer)
    {
        try
        {
            DO.Engineer doEngineer = _dal.Engineer.Read(x => x.Id == id);
            DO.Engineer doNewEngineer = ConvertBOtoDO(boEngineer);
            doNewEngineer.Id = id;
            _dal.Engineer.Update(doNewEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException($"Can't update Task with id={id}, no such Task exists!", ex);
        }

    }

    /// <summary>
    /// Resets the engineer data clears the data source.
    /// </summary>
    public void Reset()
    {
        _dal.Engineer.Reset();
    }

    /// <summary>
    /// Validates the format of an email address.
    /// Checks for the presence and position of the '@' symbol.
    /// Checks for a valid domain ending (limited to specific domains in this example).
    /// Returns true if the email format is valid, false otherwise.
    /// </summary>
    /// <param name="email">The email address to validate.</param>
    /// <returns>True if the email format is valid, false otherwise.</returns>
    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return false;
        }

        // Check for the presence of '@'
        int atIndex = email.IndexOf('@');
        if (atIndex == -1 || atIndex == 0 || atIndex == email.Length - 1)
        {
            return false;
        }

        // Check for a valid domain ending
        string domain = email.Substring(atIndex + 1);
        if (domain.EndsWith(".co.il") || domain.EndsWith(".org") || domain.EndsWith(".com"))
        {
            return true;
        }

        return false;
    }


    /// <summary>
    /// Converts a BO.Engineer object to a DO.Engineer object.
    /// </summary>
    /// <param name="boEngineer">The BO.Engineer object to convert.</param>
    /// <returns>A new DO.Engineer object with values copied from the BO.Engineer.</returns>
    private DO.Engineer ConvertBOtoDO(BO.Engineer boEngineer)
    {
        return new DO.Engineer
        {
            Id = boEngineer.Id,
            Name = boEngineer.Name,
            Email = boEngineer.Email,
            EngineerExperience = MapToDO(boEngineer.Level), // Assuming the enum values match between BO and DO
            Cost = boEngineer.Cost
        };
    }


    /// <summary>
    /// Converts a DO.Engineer object to a BO.Engineer object.
    /// </summary>
    /// <param name="doEngineer">The DO.Engineer object to convert.</param>
    /// <returns>A new BO.Engineer object with values copied from the DO.Engineer.</returns>
    private BO.Engineer ConvertDOtoBO(DO.Engineer doEngineer)
    {
        BO.Engineer engineer = new BO.Engineer
        (
            doEngineer.Id,
            doEngineer.Name,
            doEngineer.Email,
            MapFromDO(doEngineer.EngineerExperience), // Assuming the enum values match between DO and BO
            doEngineer.Cost
        );

        try
        {
            Func<DO.Task, bool> filterTask = (task) => task.EngineerID == doEngineer.Id;
            DO.Task? task = _dal.Task.Read(filterTask);
            Func<DO.Dependency, bool> depFilter = dependency => dependency.DependentTask == task.Id;

            BO.Task boTask = new BO.Task()
            {
                Id = task.Id,
                Name = task.NickName,
                Description = task.Description,
                DateCreated = task.DateCreated,
                ProjectedStartDate = task.ProjectedStartDate,
                ProjectedEndDate = task.ProjectedStartDate + task.Duration,
                ActualStartDate = task.ActualStartTime,
                DeadLine = task.DeadLine,
                ActualEndDate = task.ActualEndDate,
                Deliverable = task.Deliverables,
                Dependencies = _dal.Dependency.ReadAll(depFilter)
                .Select(dep => new TaskInList(dep.DependentOnTask, null, null, BO.Enums.TaskStatus.Unscheduled)).ToList(),
                EngineerForTask = engineer,//get engineer based off of the ID
                Level = (BO.Enums.ExperienceLevel)task.DifficultyLevel,
                Notes = task.Notes,
            };

            engineer.Task = boTask;
        }
        catch (Exception ex)
        {

        }

       

        return engineer; 

    }

    public static Enums.ExperienceLevel MapFromDO(DO.Enums.ExperienceLevel doLevel)
    {
        return doLevel switch
        {
            DO.Enums.ExperienceLevel.Novice => Enums.ExperienceLevel.Novice,
            DO.Enums.ExperienceLevel.AdvancedBeginner => Enums.ExperienceLevel.AdvancedBeginner,
            DO.Enums.ExperienceLevel.Competent => Enums.ExperienceLevel.Competent,
            DO.Enums.ExperienceLevel.Proficient => Enums.ExperienceLevel.Proficient,
            DO.Enums.ExperienceLevel.Expert => Enums.ExperienceLevel.Expert,
            _ => Enums.ExperienceLevel.None, // Default case if not matched
        };
    }

    public static DO.Enums.ExperienceLevel MapToDO(Enums.ExperienceLevel boLevel)
    {
        return boLevel switch
        {
            Enums.ExperienceLevel.Novice => DO.Enums.ExperienceLevel.Novice,
            Enums.ExperienceLevel.AdvancedBeginner => DO.Enums.ExperienceLevel.AdvancedBeginner,
            Enums.ExperienceLevel.Competent => DO.Enums.ExperienceLevel.Competent,
            Enums.ExperienceLevel.Proficient => DO.Enums.ExperienceLevel.Proficient,
            Enums.ExperienceLevel.Expert => DO.Enums.ExperienceLevel.Expert,
            // Handle 'None' and 'All' explicitly if needed or throw an exception
            _ => throw new InvalidOperationException($"BO.Enums.ExperienceLevel '{boLevel}' cannot be mapped to DO.Enums.ExperienceLevel")
        };
    }



}