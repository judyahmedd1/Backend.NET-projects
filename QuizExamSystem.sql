CREATE DATABASE QuizExamSystem;
GO
USE QuizExamSystem;
GO

CREATE TABLE Admins (
    AdminId   INT IDENTITY(1,1) PRIMARY KEY,
    AdminName NVARCHAR(50) NOT NULL,
    AdminEmail NVARCHAR(150) NOT NULL UNIQUE,
    AdminPassword NVARCHAR(50) NOT NULL
);

CREATE TABLE Teachers (
    TeacherId INT IDENTITY(1,1) PRIMARY KEY,
    TeacherName NVARCHAR(50) NOT NULL,
    TeacherEmail NVARCHAR(150) NOT NULL UNIQUE,
    TeacherPassword NVARCHAR(100) NOT NULL,
    TeacherTitle NVARCHAR(50)  NOT NULL
);

CREATE TABLE Students (
    StudentId INT IDENTITY(1,1) PRIMARY KEY,
    StudentName NVARCHAR(50) NOT NULL,
    StudentEmail NVARCHAR(150) NOT NULL UNIQUE,
    StudentPassword NVARCHAR(100) NOT NULL,
    StudentGrade NVARCHAR(20)  NOT NULL
);

CREATE TABLE Courses (
    CourseId INT IDENTITY(1,1) PRIMARY KEY,
    CourseName NVARCHAR(100) NOT NULL,
    NumberOfLessons INT NOT NULL CHECK (NumberOfLessons > 0),
    CourseCategory NVARCHAR(50)  NOT NULL,
    CourseDuration INT NOT NULL CHECK (CourseDuration > 0),   
    AdminId INT NOT NULL,
    -- NULL = no teacher yet
    TeacherId INT NULL,                            
    FOREIGN KEY (AdminId)   REFERENCES Admins(AdminId),
    FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId)
);

--many to many (students , courses)
CREATE TABLE Students_Courses (
    EnrollmentId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId    INT NOT NULL,
    CourseId     INT NOT NULL,
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (CourseId)  REFERENCES Courses(CourseId),
    -- no double enrollment
    UNIQUE (StudentId, CourseId)          
);

CREATE TABLE Quizzes (
    QuizId  INT IDENTITY(1,1) PRIMARY KEY,
    QuizName NVARCHAR(100) NOT NULL,
    QuizDuration INT NOT NULL CHECK (QuizDuration > 0),   
    CourseId INT NOT NULL,
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);

CREATE TABLE Questions (
    QuestionId INT IDENTITY(1,1) PRIMARY KEY,
    QuizId INT NOT NULL,
    QuestionText NVARCHAR(MAX) NOT NULL,
    QuestionType NVARCHAR(20)  NOT NULL CHECK (QuestionType IN ('TrueOrFalse', 'MultipleChoice', 'ShortAnswer')),
    CorrectAnswer NVARCHAR(200) NOT NULL,
    QuestionScore INT NOT NULL CHECK (QuestionScore > 0),
    FOREIGN KEY (QuizId) REFERENCES Quizzes(QuizId) ON DELETE CASCADE
);

CREATE TABLE QuestionOptions (
    OptionId   INT IDENTITY(1,1) PRIMARY KEY,
    QuestionId INT NOT NULL,
    OptionText NVARCHAR(200) NOT NULL,
    IsCorrect BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId) ON DELETE CASCADE
);

--many to many (students , quizzes)
CREATE TABLE QuizAttempts (
    AttemptId INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT NOT NULL,
    QuizId INT NOT NULL,
    AttemptTakenAt DATETIME NOT NULL DEFAULT GETDATE(),
    QuizTotalScore INT NOT NULL DEFAULT 0 CHECK (QuizTotalScore >= 0),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (QuizId)    REFERENCES Quizzes(QuizId),
    -- one attempt per quiz
    UNIQUE (StudentId, QuizId)          
);

--many to many (QuizAttempts , Questions)
CREATE TABLE AttemptAnswers (
    AnswerId      INT IDENTITY(1,1) PRIMARY KEY,
    AttemptId     INT NOT NULL,
    QuestionId    INT NOT NULL,
    -- null if time ran out
    StudentAnswer NVARCHAR(MAX) NULL,   
    IsCorrect BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (AttemptId)  REFERENCES QuizAttempts(AttemptId) ON DELETE CASCADE,
    FOREIGN KEY (QuestionId) REFERENCES Questions(QuestionId)
);

USE QuizExamSystem;
GO

INSERT INTO Admins (AdminName, AdminEmail, AdminPassword) VALUES
('Judy Ahmed',    'judy.ahmed@quiz.com',    'Adm@1001'),
('Hana Hatem',    'hana.hatem@quiz.com',    'Adm@1002'),
('Hala Helmy',     'hala.helmy@quiz.com',     'Adm@1003'),
('Farah Ahmed',       'farah.ahmed@quiz.com',       'Adm@1004'),
('Mohamed Hossam',     'mohamed.hossam@quiz.com',     'Adm@1005'),
('Nour Ibrahim',    'nour.ibrahim@quiz.com',    'Adm@1006'),
('Menna Sayed',      'menna.sayed@quiz.com',      'Adm@1007'),
('Dina Fathy',      'dina.fathy@quiz.com',      'Adm@1008'),
('Karim Nabil',     'karim.nabil@quiz.com',     'Adm@1009'),
('Layla Mahmoud',   'layla.mahmoud@quiz.com',   'Adm@1010'),
('Fares Ahmed',     'fares.ahmed@quiz.com',     'Adm@1011'),
('Nourhan Ayman',      'nourhan.ayman@quiz.com',      'Adm@1012'),
('Moaz Maher',    'moaz.maher@quiz.com',    'Adm@1013'),
('Sama Wael',     'sama.wael@quiz.com',     'Adm@1014'),
('Walid Fouad',     'walid.fouad@quiz.com',     'Adm@1015'),
('Salma Hossam',    'salma.hossam@quiz.com',    'Adm@1016'),
('Amr Zaki',        'amr.zaki@quiz.com',        'Adm@1017'),
('Yasmin Tawfik',   'yasmin.tawfik@quiz.com',   'Adm@1018'),
('Bassem Lotfy',    'bassem.lotfy@quiz.com',    'Adm@1019'),
('Farida Anwar',    'farida.anwar@quiz.com',    'Adm@1020');

INSERT INTO Teachers (TeacherName, TeacherEmail, TeacherPassword, TeacherTitle) VALUES
('Mahmoud Saad',     'mahmoud.saad@quiz.com',     'Tch@2001', 'Professor'),
('Hoda Kamal',       'hoda.kamal@quiz.com',       'Tch@2002', 'Doctor'),
('Ashraf Moussa',    'ashraf.moussa@quiz.com',    'Tch@2003', 'Doctor'),
('Nadia Farouk',     'nadia.farouk@quiz.com',     'Tch@2004', 'Engineer'),
('Samir Atef',       'samir.atef@quiz.com',       'Tch@2005', 'Professor'),
('Eman Wahba',       'eman.wahba@quiz.com',       'Tch@2006', 'Doctor'),
('Khaled Mansour',   'khaled.mansour@quiz.com',   'Tch@2007', 'Engineer'),
('Marwa Sherif',     'marwa.sherif@quiz.com',     'Tch@2008', 'Doctor'),
('Hossam Eldin',     'hossam.eldin@quiz.com',     'Tch@2009', 'Professor'),
('Ghada Ramzy',      'ghada.ramzy@quiz.com',      'Tch@2010', 'Engineer'),
('Ibrahim Sabry',    'ibrahim.sabry@quiz.com',    'Tch@2011', 'Doctor'),
('Reem Abdallah',    'reem.abdallah@quiz.com',    'Tch@2012', 'Doctor'),
('Fady Louis',       'fady.louis@quiz.com',       'Tch@2013', 'Engineer'),
('Soha Magdy',       'soha.magdy@quiz.com',       'Tch@2014', 'Professor'),
('Adel Shawky',      'adel.shawky@quiz.com',      'Tch@2015', 'Doctor'),
('Lamia Hegazy',     'lamia.hegazy@quiz.com',     'Tch@2016', 'Engineer'),
('Wael Barakat',     'wael.barakat@quiz.com',     'Tch@2017', 'Doctor'),
('Nermeen Yassin',   'nermeen.yassin@quiz.com',   'Tch@2018', 'Engineer'),
('Medhat Helmy',     'medhat.helmy@quiz.com',     'Tch@2019', 'Professor'),
('Alaa Mekawy',      'alaa.mekawy@quiz.com',      'Tch@2020', 'Doctor');

INSERT INTO Students (StudentName, StudentEmail, StudentPassword, StudentGrade) VALUES
('Mohamed Tamer',    'mohamed.tamer@std.com',    'Std@3001', 'Grade 10'),
('Aya Mahmoud',      'aya.mahmoud@std.com',      'Std@3002', 'Grade 10'),
('Ali Hesham',       'ali.hesham@std.com',       'Std@3003', 'Grade 11'),
('Mariam Sameh',     'mariam.sameh@std.com',     'Std@3004', 'Grade 11'),
('Hassan Ehab',      'hassan.ehab@std.com',      'Std@3005', 'Grade 12'),
('Jana Wael',        'jana.wael@std.com',        'Std@3006', 'Grade 12'),
('Ziad Osama',       'ziad.osama@std.com',       'Std@3007', 'Grade 10'),
('Malak Yehia',      'malak.yehia@std.com',      'Std@3008', 'Grade 10'),
('Adham Mounir',     'adham.mounir@std.com',     'Std@3009', 'Grade 11'),
('Habiba Sherif',    'habiba.sherif@std.com',    'Std@3010', 'Grade 11'),
('Seif Nader',       'seif.nader@std.com',       'Std@3011', 'Grade 12'),
('Farah Medhat',     'farah.medhat@std.com',     'Std@3012', 'Grade 12'),
('Yahia Fawzy',      'yahia.fawzy@std.com',      'Std@3013', 'Grade 10'),
('Rodina Adly',      'rodina.adly@std.com',      'Std@3014', 'Grade 10'),
('Belal Nasser',     'belal.nasser@std.com',     'Std@3015', 'Grade 11'),
('Nada Elsayed',     'nada.elsayed@std.com',     'Std@3016', 'Grade 11'),
('Kareem Lashin',    'kareem.lashin@std.com',    'Std@3017', 'Grade 12'),
('Judy Hakim',       'judy.hakim@std.com',       'Std@3018', 'Grade 12'),
('Eyad Selim',       'eyad.selim@std.com',       'Std@3019', 'Grade 10'),
('Tala Radwan',      'tala.radwan@std.com',      'Std@3020', 'Grade 11');

-- Last 3 courses have NO teacher (TeacherId = NULL)
INSERT INTO Courses (CourseName, NumberOfLessons, CourseCategory, CourseDuration, AdminId, TeacherId) VALUES
('C# Fundamentals',        30, 'Programming', 40, 1,  1),
('Data Structures',        25, 'Programming', 35, 1,  1),
('SQL Server Basics',      20, 'Databases',   30, 2,  2),
('Advanced SQL',           18, 'Databases',   25, 2,  2),
('Web Development',        28, 'Programming', 45, 3,  3),
('Computer Networks',      22, 'Networking',  36, 3,  4),
('Operating Systems',      24, 'Systems',     38, 4,  5),
('Digital Logic Design',   20, 'Hardware',    32, 4,  6),
('VHDL Basics',            16, 'Hardware',    24, 5,  6),
('Algorithms',             26, 'Programming', 40, 5,  7),
('Discrete Mathematics',   18, 'Mathematics', 28, 6,  8),
('Linear Algebra',         15, 'Mathematics', 22, 6,  9),
('Cybersecurity Basics',   14, 'Security',    20, 7,  10),
('Python Programming',     24, 'Programming', 34, 7,  11),
('Machine Learning Intro', 20, 'AI',          30, 8,  12),
('Computer Architecture',  22, 'Hardware',    36, 8,  13),
('Software Engineering',   20, 'Engineering', 30, 9,  14),
('Embedded Systems',       18, 'Hardware',    28, 9,  NULL),
('Cloud Computing',        16, 'Cloud',       24, 10, NULL),
('Mobile App Development', 22, 'Programming', 34, 10, NULL);

USE QuizExamSystem;
GO

-- Students_Courses (many-to-many). Students only enroll in courses that HAVE a teacher (1-17).
INSERT INTO Students_Courses (StudentId, CourseId) VALUES
(1,1),(2,1),(3,1),(4,1),(5,1),(6,1),(7,1),   
(1,3),(2,3),(3,3),(8,3),(9,3),(10,3),        
(1,2),(4,2),(11,2),
(3,4),(8,4),
(5,5),(12,5),(13,5),
(6,6),(14,6),
(7,7),(15,7),
(9,8),(16,8),
(2,10),(17,10),
(10,14),(18,14),
(11,15),(19,15),
(12,17),(20,17);

INSERT INTO Quizzes (QuizName, QuizDuration, CourseId) VALUES
('C# Basics Quiz', 20, 1),
('C# OOP Quiz', 30, 1),
('C# Collections Quiz', 25, 1),
('Data Structures Quiz', 20, 2),
('Stacks and Queues Quiz', 15, 2),
('SQL SELECT Quiz', 20, 3),
('SQL Joins Quiz', 25, 3),
('SQL Constraints Quiz', 15, 3),
('Subqueries Quiz', 30, 4),
('HTML and CSS Quiz', 20, 5),
('Networking Basics Quiz', 25, 6),
('OSI Model Quiz', 15, 6),
('Process Management Quiz', 30, 7),
('Logic Gates Quiz', 20, 8),
('Boolean Algebra Quiz', 25, 8),
('VHDL Syntax Quiz', 20, 9),
('Sorting Algorithms Quiz', 30, 10),
('Python Basics Quiz', 20, 14),
('ML Concepts Quiz', 25, 15),
('Agile Quiz', 15, 17);

-- 2 questions per quiz: QuestionId = 2*QuizId-1 and 2*QuizId
INSERT INTO Questions (QuizId, QuestionText, QuestionType, CorrectAnswer, QuestionScore) VALUES
(1,  'C# is a case-sensitive language.', 'TrueOrFalse', 'True', 5),
(1,  'Which keyword is used to declare a constant?', 'MultipleChoice', 'const', 5),
(2,  'A class can inherit from multiple classes in C#.', 'TrueOrFalse', 'False', 5),
(2,  'Which OOP concept hides data using access modifiers?', 'ShortAnswer', 'Encapsulation', 5),
(3,  'A List<T> has a fixed size.', 'TrueOrFalse', 'False', 5),
(3,  'Which collection stores key/value pairs?', 'MultipleChoice', 'Dictionary', 5),
(4,  'An array stores elements in contiguous memory.', 'TrueOrFalse', 'True', 5),
(4,  'What is the time complexity of accessing an array element by index?', 'ShortAnswer', 'O(1)', 5),
(5,  'A stack follows the FIFO principle.', 'TrueOrFalse', 'False', 5),
(5,  'Which data structure follows FIFO?', 'MultipleChoice', 'Queue', 5),
(6,  'SELECT * returns all columns of a table.', 'TrueOrFalse', 'True', 5),
(6,  'Which clause is used to filter rows?', 'ShortAnswer', 'WHERE', 5),
(7,  'INNER JOIN returns only matching rows.', 'TrueOrFalse', 'True', 5),
(7,  'Which join returns all rows from the left table?', 'MultipleChoice', 'LEFT JOIN', 5),
(8,  'A table can have more than one PRIMARY KEY.', 'TrueOrFalse', 'False', 5),
(8,  'Which constraint prevents duplicate values in a column?', 'ShortAnswer', 'UNIQUE', 5),
(9,  'A scalar subquery returns a single value.', 'TrueOrFalse', 'True', 5),
(9,  'Which operator is used with a multi-row subquery?', 'MultipleChoice', 'IN', 5),
(10, 'HTML is a programming language.', 'TrueOrFalse', 'False', 5),
(10, 'Which tag creates a hyperlink?', 'ShortAnswer', '<a>', 5),
(11, 'An IP address identifies a device on a network.', 'TrueOrFalse', 'True', 5),
(11, 'Which device connects different networks?', 'MultipleChoice', 'Router', 5),
(12, 'The OSI model has 7 layers.', 'TrueOrFalse', 'True', 5),
(12, 'Which OSI layer handles routing?', 'ShortAnswer', 'Network', 5),
(13, 'A process is a program in execution.', 'TrueOrFalse', 'True', 5),
(13, 'Which scheduling algorithm gives each process a fixed time slice?', 'MultipleChoice', 'Round Robin', 5),
(14, 'The output of an AND gate is 1 only if all inputs are 1.', 'TrueOrFalse', 'True', 5),
(14, 'Which gate is a universal gate along with NOR?', 'ShortAnswer', 'NAND', 5),
(15, 'A + A = 2A in Boolean algebra.', 'TrueOrFalse', 'False', 5),
(15, 'What is the result of A AND 0?', 'MultipleChoice', '0', 5),
(16, 'VHDL is a hardware description language.', 'TrueOrFalse', 'True', 5),
(16, 'Which keyword defines the internal behavior of an entity?', 'ShortAnswer', 'architecture', 5),
(17, 'Bubble sort has O(n log n) worst-case time.', 'TrueOrFalse', 'False', 5),
(17, 'Which sorting algorithm uses divide and conquer?', 'MultipleChoice', 'Merge Sort', 5),
(18, 'Python uses curly braces to define code blocks.', 'TrueOrFalse', 'False', 5),
(18, 'Which function prints output in Python?', 'ShortAnswer', 'print', 5),
(19, 'Supervised learning uses labeled data.', 'TrueOrFalse', 'True', 5),
(19, 'Which of these is a supervised learning task?', 'MultipleChoice', 'Classification', 5),
(20, 'Scrum is an Agile framework.', 'TrueOrFalse', 'True', 5),
(20, 'What is a short development cycle in Scrum called?', 'ShortAnswer', 'Sprint', 5);

-- Options only for MultipleChoice questions (ids 2,6,10,14,18,22,26,30,34,38)
INSERT INTO QuestionOptions (QuestionId, OptionText) VALUES
(2,'const'),(2,'var'),(2,'final'),
(6,'Dictionary'),(6,'Stack'),(6,'Queue'),
(10,'Queue'),(10,'Stack'),(10,'Tree'),
(14,'LEFT JOIN'),(14,'INNER JOIN'),(14,'CROSS JOIN'),
(18,'IN'),(18,'='),(18,'LIKE'),
(22,'Router'),(22,'Hub'),(22,'Cable'),
(26,'Round Robin'),(26,'FCFS'),(26,'SJF'),
(30,'0'),(30,'1'),(30,'A'),
(34,'Merge Sort'),(34,'Bubble Sort'),(34,'Insertion Sort'),
(38,'Classification'),(38,'Clustering'),(38,'Compression');

-- Quizzes 3, 8, 16, 20 have NO attempts (use them to test sp_DeleteQuiz)
INSERT INTO QuizAttempts (StudentId, QuizId, QuizTotalScore) VALUES
(1,1,10),(2,1,5),(3,1,0),(4,2,10),(5,2,5),
(1,6,10),(2,6,5),(3,7,10),(8,6,5),(9,7,5),
(1,4,10),(4,4,0),(11,5,10),(3,9,10),(8,9,5),
(5,10,10),(6,11,5),(14,12,5),(7,13,10),(9,14,10),
(16,15,0),(2,17,10),(10,18,10),(11,19,5);

-- AttemptId = row order above; NULL = time ran out
INSERT INTO AttemptAnswers (AttemptId, QuestionId, StudentAnswer) VALUES
(1,1,'True'),(1,2,'const'),
(2,1,'True'),(2,2,'var'),
(3,1,'False'),(3,2,NULL),
(4,3,'False'),(4,4,'Encapsulation'),
(5,3,'True'),(5,4,'Encapsulation'),
(6,11,'True'),(6,12,'WHERE'),
(7,11,'True'),(7,12,'HAVING'),
(8,13,'True'),(8,14,'LEFT JOIN'),
(9,11,'False'),(9,12,'WHERE'),
(10,13,'True'),(10,14,'INNER JOIN'),
(11,7,'True'),(11,8,'O(1)'),
(12,7,'False'),(12,8,'O(n)'),
(13,9,'False'),(13,10,'Queue'),
(14,17,'True'),(14,18,'IN'),
(15,17,'True'),(15,18,'='),
(16,19,'False'),(16,20,'<a>'),
(17,21,'True'),(17,22,'Hub'),
(18,23,'True'),(18,24,'Transport'),
(19,25,'True'),(19,26,'Round Robin'),
(20,27,'True'),(20,28,'NAND'),
(21,29,'True'),(21,30,'1'),
(22,33,'False'),(22,34,'Merge Sort'),
(23,35,'False'),(23,36,'print'),
(24,37,'True'),(24,38,'Clustering');

USE QuizExamSystem;
GO                             

--type: view (fixed query, no input)
CREATE VIEW CoursesWithTeachers AS
SELECT c.CourseName, t.TeacherName, c.CourseCategory
FROM Courses c
INNER JOIN Teachers t ON c.TeacherId = t.TeacherId;
GO     

--type: view (no input)
--left join keeps unmatched rows, keep all courses
CREATE VIEW QuizQuestionCount AS
SELECT qz.QuizName, COUNT(q.QuestionId) AS NumberOfQuestions
FROM Quizzes qz
LEFT JOIN Questions q ON qz.QuizId = q.QuizId
GROUP BY qz.QuizId, qz.QuizName;
GO


--type: table Function (needs QuizId, returns rows) 
CREATE FUNCTION GetQuizQuestions (@QuizId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT QuestionId, QuestionText, QuestionType, CorrectAnswer, QuestionScore
    FROM Questions
    WHERE QuizId = @QuizId
);
GO

--type: table function (needs CourseId, returns rows) 
CREATE FUNCTION GetCourseStudents (@CourseId INT)
RETURNS TABLE
AS
RETURN
(
    SELECT s.StudentName, c.CourseName
    FROM Students_Courses sc
    INNER JOIN Students s ON sc.StudentId = s.StudentId
    INNER JOIN Courses c  ON sc.CourseId  = c.CourseId
    WHERE sc.CourseId = @CourseId
);
GO

--type: scalar function (needs CourseId, returns ONE value) 
CREATE FUNCTION GetCourseQuizCount (@CourseId INT)
RETURNS INT
AS
BEGIN
    DECLARE @QuizCount INT;
    SELECT @QuizCount = COUNT(*) FROM Quizzes WHERE CourseId = @CourseId;
    RETURN @QuizCount;
END;
GO

--type: view (no input)
--LEFT JOIN keeps courses where TeacherId is NULL. 
CREATE VIEW AllCoursesWithTeachers AS
SELECT c.CourseName, t.TeacherName
FROM Courses c
LEFT JOIN Teachers t ON c.TeacherId = t.TeacherId;
GO
 

--type: stored procedure (changes data) 
CREATE PROCEDURE UpdateCourseDuration
    @CourseId INT,
    @NewDuration INT
AS
BEGIN
    UPDATE Courses
    SET CourseDuration = @NewDuration
    WHERE CourseId = @CourseId;
END;
GO
 

--type: stored procedure (changes data + condition)
--on delete cascade deletes the quiz's questions and options automatically
CREATE PROCEDURE DeleteQuiz
    @QuizId INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM QuizAttempts WHERE QuizId = @QuizId)
        PRINT 'Quiz cannot be deleted: students have taken it.';
    ELSE
        DELETE FROM Quizzes WHERE QuizId = @QuizId;
END;
GO
 

--type: scalar function (returns one value) 
CREATE FUNCTION GetAverageQuizDuration ()
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @AverageDuration DECIMAL(10,2);
    SELECT @AverageDuration = AVG(CAST(QuizDuration AS DECIMAL(10,2))) FROM Quizzes;
    RETURN @AverageDuration;
END;
GO
   
CREATE VIEW AverageScorePerQuiz AS
SELECT qz.QuizName, AVG(CAST(qa.QuizTotalScore AS DECIMAL(10,2))) AS AverageScore
FROM Quizzes qz
INNER JOIN QuizAttempts qa ON qz.QuizId = qa.QuizId
GROUP BY qz.QuizId, qz.QuizName;
GO
 

CREATE VIEW StudentAnswersDetails AS
SELECT s.StudentName, qz.QuizName, aa.StudentAnswer, q.CorrectAnswer, qa.QuizTotalScore AS QuizTotalScore
FROM AttemptAnswers aa
INNER JOIN QuizAttempts qa ON aa.AttemptId  = qa.AttemptId
INNER JOIN Students s      ON qa.StudentId  = s.StudentId
INNER JOIN Quizzes qz      ON qa.QuizId     = qz.QuizId
INNER JOIN Questions q     ON aa.QuestionId = q.QuestionId;
GO


CREATE VIEW CoursesWithMoreThan5Students AS
SELECT c.CourseName, COUNT(sc.StudentId) AS NumberOfStudents
FROM Courses c
INNER JOIN Students_Courses sc ON c.CourseId = sc.CourseId
GROUP BY c.CourseId, c.CourseName
HAVING COUNT(sc.StudentId) > 5;
GO
 

CREATE VIEW CoursesWithQuizzes AS
SELECT c.CourseId, c.CourseName
FROM Courses c
WHERE EXISTS (SELECT 1 FROM Quizzes qz WHERE qz.CourseId = c.CourseId);
GO
 

--type: stored procedure (inserts data)
CREATE PROCEDURE AddAdmin
    @AdminName NVARCHAR(50),
    @AdminEmail NVARCHAR(150),
    @AdminPassword NVARCHAR(50)
AS
BEGIN
    INSERT INTO Admins (AdminName, AdminEmail, AdminPassword)
    VALUES (@AdminName, @AdminEmail, @AdminPassword);
END;
GO
 

--type: table function (needs StudentId, returns rows)
CREATE FUNCTION GetStudentPerformance (@StudentId INT)
RETURNS @Report TABLE
(
    StudentName NVARCHAR(50),
    CourseName  NVARCHAR(100),
    QuizName    NVARCHAR(100),
    TotalScore  INT,
    Result      NVARCHAR(10)
)
AS
BEGIN
    DECLARE @PassPercentage DECIMAL(3,2) = 0.60;
    DECLARE @PassText NVARCHAR(10) = 'Pass';
    DECLARE @FailText NVARCHAR(10) = 'Fail';
 
    INSERT INTO @Report
    SELECT s.StudentName, c.CourseName, qz.QuizName, qa.QuizTotalScore,
           CASE WHEN qa.QuizTotalScore >=
                     @PassPercentage * (SELECT SUM(q.QuestionScore)
                                        FROM Questions q
                                        WHERE q.QuizId = qz.QuizId)
                THEN @PassText ELSE @FailText END
    FROM QuizAttempts qa
    INNER JOIN Students s ON qa.StudentId = s.StudentId
    INNER JOIN Quizzes qz ON qa.QuizId    = qz.QuizId
    INNER JOIN Courses c  ON qz.CourseId  = c.CourseId
    WHERE qa.StudentId = @StudentId;
 
    RETURN;
END;
GO
