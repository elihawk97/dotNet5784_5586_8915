# Task Management System Project

## Project Overview
Developed a comprehensive Windows-based task management system using a multi-tier architecture approach. The system enables efficient project task management through distinct administrator and engineer interfaces.

## Key Features
- **Multi-Tier Architecture**: Implemented a three-tier architecture separating data access, business logic, and presentation layers
- **Dual Interface System**:
  - Administrator Interface: Full control over task management, engineer assignments, and milestone creation
  - Engineer Interface: Task viewing and progress tracking capabilities

## Technical Implementation

### Data Layer (DAL)
- Implemented core data entities (Tasks, Engineers, Dependencies)
- Created CRUD operations for all entities
- Developed XML-based data persistence
- Implemented generic interfaces using IEnumerable
- Utilized LINQ for data processing

### Business Layer (BL)
- Implemented business logic for task scheduling and management
- Created milestone tracking and dependency management
- Developed automated project scheduling algorithms
- Implemented real-time status tracking and updates
- Added validation and data integrity checks

### Presentation Layer (GUI)
- Created intuitive Windows-based interface using XAML
- Implemented Gantt chart visualization for project timeline
- Developed filtering and sorting capabilities
- Added real-time project progress tracking
- Created role-based access control

## Key Technical Features
- **Data Management**: 
  - XML-based persistence
  - LINQ query optimization
  - Generic interface implementation
  
- **Business Logic**:
  - Automated scheduling algorithms
  - Dependency resolution
  - Resource allocation
  - Status tracking
  
- **User Interface**:
  - XAML-based GUI
  - Data binding
  - Interactive Gantt charts
  - Real-time updates

## Project Outcomes
- Successfully implemented all core requirements
- Created a functional task management system
- Developed efficient project scheduling capabilities
- Implemented robust data persistence
- Created intuitive user interfaces for both administrators and engineers

## Technologies Used
- C# (.NET)
- WPF/XAML
- XML
- LINQ
- Multi-tier Architecture
