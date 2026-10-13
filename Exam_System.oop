using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_System
{
    public enum QuestionType { 
        TrueOrFalse, MultipleChoice, ShortAnswer 
    }
    public enum UserRole { Admin, Teacher, Student }
    public static class ErrorMessages
    {
        //users class
        public const string EmptyName = "User's name can't be empty";
        public const string InvalidEmail = "Email is invalid";
        public const string PasswordTooShort = "Password is too short";
        public const string EmptyTitle = "Title can't be empty";
        public const string InvalidGrade = "Grade is invalid";

        //question class
        public const string EmptyQuestionText = "Question text can't be empty";
        public const string InvalidMark = "Mark must be positive";
        public const string NotEnoughOptions = "A multiple choice question needs more options";
        public const string EmptyOption = "Option can't be empty";
        public const string InvalidCorrectOption = "Correct option number is out of range";
        public const string EmptyCorrectAnswer = "Correct answer can't be empty";

        //quiz class 
        public const string EmptyQuizName = "Quiz name can't be empty";
        public const string InvalidDuration = "Duration must be positive";
        public const string InvalidPercentage = "Pass percentage is out of range";
        public const string NullQuestion = "Question is required";
        public const string QuizAlreadyTaken = "Quiz can't be edited after students took it";
        public const string QuizNeedsCourse = "Quiz needs a course";
        public const string QuizNeedsTeacher = "Quiz needs a teacher";
        public const string TeacherNotInCourse = "Teacher is not assigned to this course";

        //course class
        public const string EmptyCourseName = "Course name can't be empty";
        public const string EmptyCategory = "Category can't be empty";
        public const string InvalidLessons = "Number of lessons must be a positive number";
        public const string NullAdmin = "Course must be created by an admin";
        public const string NullStudent = "Student is required";
        public const string NullQuiz = "Quiz is required";
        public const string QuizWrongCourse = "Quiz belongs to another course";
        public const string CourseHasTeacher = "Course already has a teacher";
        public const string CourseHasNoTeacher = "Course has no teacher yet";
        public const string CourseHasAttempts = "Students already took quizzes of this course";
        public const string AlreadyEnrolled = "Student is already enrolled";
        public const string NullTeacher = "Teacher is unassigned";

        //quiz attempt class
        public const string QuizHasNoQuestions = "Quiz has no questions";
        public const string NotEnrolled = "Student is not enrolled in this course";
        public const string AttemptFinished = "Attempt is already finished";

        //UserManager (register, login, unique email)
        public const string EmailTaken = "Email is already registered";

        // managers class
        public const string CourseNotFound = "Course was not found";
        public const string QuizNotFound = "Quiz was not found";
        public const string TeacherNotAssigned = "Teacher is not assigned to this course";
        public const string TeacherDidNotCreateQuiz = "Teacher did not create this quiz";
        public const string StudentAlreadyTookQuiz = "Student already took this quiz";

        // console application
        public const string InvalidMenuChoice = "Invalid menu choice";
        public const string LoginFailed = "Email or password is incorrect";
        public const string InvalidQuestionNumber = "Question number is out of range";
        public const string TrueOrFalseOnly = "Please enter true or false";
    }
    public static class ValidationRules
    {
        public const int MinPasswordLength = 8;
        public const string EmailSymbol = "@";
        public const int MinGrade = 1;
        public const int MaxGrade = 100;
        public const int MinQuestionMark = 1;
        public const int MinOptions = 2;
        public const int FirstOptionNumber = 1;
        public const int MinDuration = 1;
        public const int MinLessons = 1;
        public const int MinPercentage = 1;
        public const int MaxPercentage = 100;
        public const int NoMarks = 0;
    }
    public static class DisplayFormats
    {
        public const string QuestionFormat = "Question {0} has {1} marks";
        public const string OptionFormat = "{0}. {1}";
        public const string QuizFormat = "Quiz: {0}\nDuration: {1} minutes\nTaken by: {2}\nQuestions: {3}";
        public const string CourseBasicFormat = "Course: {0}\nCategory: {1}\nDuration: {2} hours";
        public const string CourseFullFormat = CourseBasicFormat + "\nLessons: {3}\nQuizzes: {4}\nTeacher: {5}";
        public const string NoTeacher = "Not assigned";
        public const string AttemptFormat = "Quiz: {0}\nCourse: {1}\nDuration: {2} min\nScore: {3}/{4}\nResult: {5}";
        public const string AnswerFormat = "\n{0}\nYour answer: {1}\nCorrect answer: {2}";
    }
    public abstract class User
    {
        private static int _IdCounter = 0;

        private string _UserName;
        private string _UserEmail;
        private string _UserPassword;

        public abstract UserRole Role { get; }

        public int Id { get; }

        public string Name
        {
            get { return _UserName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.EmptyName);
                _UserName = value;
            }
        }

        public string Email
        {
            get { return _UserEmail; }
            set
            {
                if (string.IsNullOrEmpty(value) || !value.Contains(ValidationRules.EmailSymbol))
                    throw new ArgumentException(ErrorMessages.InvalidEmail);
                _UserEmail = value;
            }
        }

        public string Password
        {
            get { return _UserPassword; }
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < ValidationRules.MinPasswordLength)
                    throw new ArgumentException(ErrorMessages.PasswordTooShort);
                _UserPassword = value;
            }
        }

        protected User(string name, string email, string password)
        {
            _IdCounter++;
            Id = _IdCounter;
            Name = name;        
            Email = email;
            Password = password;
        }

        public bool CheckCredentials(string email, string password)
        {
            return Email == email && Password == password;
        }
    }
    public class Admin : User
    {
        public Admin(string name, string email, string password)
            : base(name, email, password) { }
        public override UserRole Role { get { return UserRole.Admin; } }
    }
    public class Teacher : User
    {
        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.EmptyTitle);
                _title = value;
            }
        }

        public Teacher(string name, string email, string password, string title)
            : base(name, email, password)
        {
            Title = title;
        }
        public override UserRole Role { get { return UserRole.Teacher; } }
    }
    public class Student : User
    {
        private int _grade;

        public int Grade
        {
            get { return _grade; }
            set
            {
                if (value < ValidationRules.MinGrade || value > ValidationRules.MaxGrade)
                    throw new ArgumentException(ErrorMessages.InvalidGrade);
                _grade = value;
            }
        }

        public Student(string name, string email, string password, int grade)
            : base(name, email, password)
        {
            Grade = grade;
        }
        public override UserRole Role { get { return UserRole.Student; } }
    }
    public static class SeedData
    {
        public const string AdminName = "Admin";
        public const string AdminEmail = "admin@quiz.com";
        public const string AdminPassword = "Admin1234";
    }
    public class UserManager
    {
        private List<User> _users = new List<User>();
        private List<Teacher> _teachers = new List<Teacher>();
        private List<Student> _students = new List<Student>();

        public UserManager()
        {
            // no "add admin" feature exists, so the admin is created here
            _users.Add(new Admin(SeedData.AdminName, SeedData.AdminEmail, SeedData.AdminPassword));
        }

        public bool IsEmailTaken(string email)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Email == email)
                    return true;
            }
            return false;
        }

        public Teacher RegisterTeacher(string name, string email, string password, string title)
        {
            if (IsEmailTaken(email))
                throw new InvalidOperationException(ErrorMessages.EmailTaken);

            Teacher teacher = new Teacher(name, email, password, title);
            _users.Add(teacher);
            _teachers.Add(teacher);
            return teacher;
        }

        public Student RegisterStudent(string name, string email, string password, int grade)
        {
            if (IsEmailTaken(email))
                throw new InvalidOperationException(ErrorMessages.EmailTaken);

            Student student = new Student(name, email, password, grade);
            _users.Add(student);
            _students.Add(student);
            return student;
        }

        // returns the user, or null if email/password are wrong
        public User Login(string email, string password)
        {
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].CheckCredentials(email, password))
                    return _users[i];
            }
            return null;
        }

        public List<Teacher> GetTeachers() { return new List<Teacher>(_teachers); }
        public List<Student> GetStudents() { return new List<Student>(_students); }
    }
    public interface IDisplayable { string GetDetails(); }          
    public interface IGradable { bool IsCorrect(string answer); }
    public abstract class Question : IDisplayable, IGradable
    {
        private static int _IdCounter = 0;
        private string _text;
        private int _mark;

        public int Id { get; }
        public QuestionType Type { get; }

        public string Text
        {
            get { return _text; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.EmptyQuestionText);
                _text = value;
            }
        }

        public int Mark
        {
            get { return _mark; }
            set
            {
                if (value < ValidationRules.MinQuestionMark)
                    throw new ArgumentException(ErrorMessages.InvalidMark);
                _mark = value;
            }
        }
        protected Question(string text, int mark, QuestionType type)
        {
            _IdCounter++;
            Id = _IdCounter;
            Text = text;
            Mark = mark;
            Type = type;
        }

        // each type decides
        public abstract bool IsCorrect(string answer);      
        public abstract string GetCorrectAnswer();

        public virtual string GetDetails()
        {
            return string.Format(DisplayFormats.QuestionFormat, Text, Mark);
        }
    }
    public class TrueOrFalseQuestion : Question
    {
        private bool _CorrectAnswer;

        public TrueOrFalseQuestion(string text, int mark, bool correctanswer)
            : base(text, mark, QuestionType.TrueOrFalse)
        {
            _CorrectAnswer = correctanswer;
        }

        public override bool IsCorrect(string answer)
        {
            if (string.IsNullOrEmpty(answer))
                return false;
            return answer.ToLower() == _CorrectAnswer.ToString().ToLower();
        }

        public override string GetCorrectAnswer()
        {
            return _CorrectAnswer.ToString();
        }
    }
    public class MultipleChoiceQuestion : Question
    {
        private List<string> _options;
        private int _CorrectOptionNumber;   

        public MultipleChoiceQuestion(string text, int mark, List<string> options, int correctOptionNumber)
            : base(text, mark, QuestionType.MultipleChoice)
        {
            if (options == null || options.Count < ValidationRules.MinOptions)
                throw new ArgumentException(ErrorMessages.NotEnoughOptions);

            for (int i = 0; i < options.Count; i++)
            {
                if (string.IsNullOrEmpty(options[i]))
                    throw new ArgumentException(ErrorMessages.EmptyOption);
            }

            if (correctOptionNumber < ValidationRules.FirstOptionNumber ||
                correctOptionNumber > options.Count)
                throw new ArgumentException(ErrorMessages.InvalidCorrectOption);

            _options = new List<string>(options);   
            _CorrectOptionNumber = correctOptionNumber;
        }

        public override bool IsCorrect(string answer)
        {
            int chosen;
            if (!int.TryParse(answer, out chosen))
                return false;
            return chosen == _CorrectOptionNumber;
        }

        public override string GetCorrectAnswer()
        {
            return _options[_CorrectOptionNumber - ValidationRules.FirstOptionNumber];
        }

        public override string GetDetails()
        {
            string details = base.GetDetails();
            for (int i = 0; i < _options.Count; i++)
            {
                details += string.Format(DisplayFormats.OptionFormat,i + ValidationRules.FirstOptionNumber,
                    _options[i]);
            }
            return details;
        }
    }
    public class ShortAnswerQuestion : Question
    {
        private string _CorrectAnswer;

        public ShortAnswerQuestion(string text, int mark, string correctAnswer)
            : base(text, mark, QuestionType.ShortAnswer)
        {
            if (string.IsNullOrEmpty(correctAnswer))
                throw new ArgumentException(ErrorMessages.EmptyCorrectAnswer);
            _CorrectAnswer = correctAnswer;
        }

        public override bool IsCorrect(string answer)
        {
            if (string.IsNullOrEmpty(answer))
                return false;
            return answer.Trim().ToLower() == _CorrectAnswer.Trim().ToLower();
        }

        public override string GetCorrectAnswer()
        {
            return _CorrectAnswer;
        }
    }
    public class Course : IDisplayable
    {
        private static int _IdCounter = 0;
        private string _name;
        private string _category;
        private int _numberOfLessons;
        private int _durationInHours;
        private List<Student> _students = new List<Student>();
        private List<Quiz> _quizzes = new List<Quiz>();

        public int Id { get; }
        public Admin CreatedBy { get; }
        // null = unassigned
        public Teacher Teacher { get; private set; }     
        public bool HasTeacher { get { return Teacher != null; } }
        public int StudentCount { get { return _students.Count; } }
        public int QuizCount { get { return _quizzes.Count; } }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.EmptyCourseName);
                _name = value;
            }
        }

        public string Category
        {
            get { return _category; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.EmptyCategory);
                _category = value;
            }
        }

        public int NumberOfLessons
        {
            get { return _numberOfLessons; }
            set
            {
                if (value < ValidationRules.MinLessons)
                    throw new ArgumentException(ErrorMessages.InvalidLessons);
                _numberOfLessons = value;
            }
        }

        public int DurationInHours
        {
            get { return _durationInHours; }
            set
            {
                if (value < ValidationRules.MinDuration)
                    throw new ArgumentException(ErrorMessages.InvalidDuration);
                _durationInHours = value;
            }
        }

        public Course(string name, string category, int numberOfLessons, int durationInHours, Admin createdBy)
        {
            if (createdBy == null)
                throw new ArgumentException(ErrorMessages.NullAdmin);

            _IdCounter++;
            Id = _IdCounter;
            Name = name;
            Category = category;
            NumberOfLessons = numberOfLessons;
            DurationInHours = durationInHours;
            CreatedBy = createdBy;
        }

        public void AssignTeacher(Teacher teacher)
        {
            if (teacher == null)
                throw new ArgumentException(ErrorMessages.NullTeacher);
            if (HasTeacher)
                throw new InvalidOperationException(ErrorMessages.CourseHasTeacher);
            Teacher = teacher;
        }

        public void ReleaseTeacher()
        {
            if (HasStudentTakenAnyQuiz())
                throw new InvalidOperationException(ErrorMessages.CourseHasAttempts);
            Teacher = null;
        }

        public void EnrollStudent(Student student)
        {
            if (student == null)
                throw new ArgumentException(ErrorMessages.NullStudent);
            if (!HasTeacher)
                throw new InvalidOperationException(ErrorMessages.CourseHasNoTeacher);
            if (_students.Contains(student))
                throw new InvalidOperationException(ErrorMessages.AlreadyEnrolled);
            _students.Add(student);
        }

        public void AddQuiz(Quiz quiz)
        {
            if (quiz == null)
                throw new ArgumentException(ErrorMessages.NullQuiz);
            if (quiz.Course != this)
                throw new ArgumentException(ErrorMessages.QuizWrongCourse);
            _quizzes.Add(quiz);
        }

        public bool HasStudentTakenAnyQuiz()
        {
            for (int i = 0; i < _quizzes.Count; i++)
            {
                if (_quizzes[i].StudentsTaken > 0)
                    return true;
            }
            return false;
        }

        public List<Student> GetStudents() { return new List<Student>(_students); }
        public List<Quiz> GetQuizzes() { return new List<Quiz>(_quizzes); }

        //for teachers viewing an unassigned course: no lessons, no quizzes
        public string GetBasicDetails()
        {
            return string.Format(DisplayFormats.CourseBasicFormat, Name, Category, DurationInHours);
        }

        public string GetDetails()
        {
            string teacherName = HasTeacher ? Teacher.Name : DisplayFormats.NoTeacher;
            return string.Format(DisplayFormats.CourseFullFormat,
                Name, Category, DurationInHours, NumberOfLessons, QuizCount, teacherName);
        }
        public bool IsEnrolled(Student student)
        {
            return _students.Contains(student);
        }
    }
    public class Quiz : IDisplayable
    {
        private static int _IdCounter = 0;
        private string _name;
        private int _durationInMinutes;
        private int _passPercentage;
        private List<Question> _questions = new List<Question>();

        public int Id { get; }
        public Course Course { get; }
        public Teacher CreatedBy { get; }
        public int StudentsTaken { get; private set; }
        public int QuestionCount { get { return _questions.Count; } }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException(ErrorMessages.EmptyQuizName);
                _name = value;
            }
        }

        public int DurationInMinutes
        {
            get { return _durationInMinutes; }
            set
            {
                if (value < ValidationRules.MinDuration)
                    throw new ArgumentException(ErrorMessages.InvalidDuration);
                _durationInMinutes = value;
            }
        }

        public int PassPercentage
        {
            get { return _passPercentage; }
            set
            {
                if (value < ValidationRules.MinPercentage || value > ValidationRules.MaxPercentage)
                    throw new ArgumentException(ErrorMessages.InvalidPercentage);
                _passPercentage = value;
            }
        }

        public Quiz(string name, int durationInMinutes, int passPercentage, Course course, Teacher createdBy)
        {
            if (course == null)
                throw new ArgumentException(ErrorMessages.QuizNeedsCourse);
            if (createdBy == null)
                throw new ArgumentException(ErrorMessages.QuizNeedsTeacher);
            if (course.Teacher != createdBy)
                throw new ArgumentException(ErrorMessages.TeacherNotInCourse);

            _IdCounter++;
            Id = _IdCounter;
            Name = name;
            DurationInMinutes = durationInMinutes;
            PassPercentage = passPercentage;
            Course = course;
            CreatedBy = createdBy;
        }

        // lets you use a Quiz object like an array,indexer gives read access to one question without exposing the list
        public Question this[int index]
        {
            get { return _questions[index]; }
        }

        public void AddQuestion(Question question)
        {
            CheckEditable();
            if (question == null)
                throw new ArgumentException(ErrorMessages.NullQuestion);
            _questions.Add(question);
        }

        public bool RemoveQuestion(Question question)
        {
            CheckEditable();
            return _questions.Remove(question);
        }

        public int GetTotalMarks()
        {
            int total = 0;
            for (int i = 0; i < _questions.Count; i++)
            {
                total += _questions[i].Mark;
            }
            return total;
        }

        public void RegisterAttempt()
        {
            StudentsTaken++;
        }

        private void CheckEditable()
        {
            if (StudentsTaken > 0)
                throw new InvalidOperationException(ErrorMessages.QuizAlreadyTaken);
        }

        public string GetDetails()
        {
            return string.Format(DisplayFormats.QuizFormat, Name, DurationInMinutes, StudentsTaken, QuestionCount);
        }
    }
    public enum QuizResult { Pass, Fail }

    public class StudentAnswer
    {
        public Question Question { get; }
        public string Answer { get; }
        public bool IsCorrect { get; }

        public int MarksEarned
        {
            get { return IsCorrect ? Question.Mark : ValidationRules.NoMarks; }
        }

        public StudentAnswer(Question question, string answer)
        {
            if (question == null)
                throw new ArgumentException(ErrorMessages.NullQuestion);

            Question = question;
            Answer = answer;
            IsCorrect = question.IsCorrect(answer);   // polymorphism
        }
    }
    public class QuizAttempt : IDisplayable
    {
        private List<StudentAnswer> _answers = new List<StudentAnswer>();

        public Student Student { get; }
        public Quiz Quiz { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; private set; }
        public int Score { get; private set; }
        public bool IsFinished { get; private set; }

        public QuizAttempt(Student student, Quiz quiz)
        {
            if (student == null)
                throw new ArgumentException(ErrorMessages.NullStudent);
            if (quiz == null)
                throw new ArgumentException(ErrorMessages.NullQuiz);
            if (quiz.QuestionCount == 0)
                throw new InvalidOperationException(ErrorMessages.QuizHasNoQuestions);
            if (!quiz.Course.IsEnrolled(student))
                throw new InvalidOperationException(ErrorMessages.NotEnrolled);

            Student = student;
            Quiz = quiz;
            StartTime = DateTime.Now;
            // the quiz is now locked for editing
            quiz.RegisterAttempt();       
        }

        public bool IsTimeUp()
        {
            return (DateTime.Now - StartTime).TotalMinutes >= Quiz.DurationInMinutes;
        }

        public Question GetCurrentQuestion()
        {
            if (IsFinished)
                throw new InvalidOperationException(ErrorMessages.AttemptFinished);
            // indexer: next unanswered question
            return Quiz[_answers.Count];     
        }

        // returns false if time was up (answer is NOT counted)
        public bool SubmitAnswer(string answer)
        {
            if (IsFinished)
                throw new InvalidOperationException(ErrorMessages.AttemptFinished);

            if (IsTimeUp())
            {
                Finish();
                return false;
            }

            _answers.Add(new StudentAnswer(Quiz[_answers.Count], answer));

            if (_answers.Count == Quiz.QuestionCount)
                Finish();
            return true;
        }

        public void Finish()
        {
            if (IsFinished)
                return;

            // unanswered questions get an empty answer (0 marks)
            for (int i = _answers.Count; i < Quiz.QuestionCount; i++)
            {
                _answers.Add(new StudentAnswer(Quiz[i], string.Empty));
            }

            Score = ValidationRules.NoMarks;
            for (int i = 0; i < _answers.Count; i++)
            {
                Score += _answers[i].MarksEarned;
            }

            EndTime = DateTime.Now;
            IsFinished = true;
        }

        public int GetPercentage()
        {
            return Score * ValidationRules.MaxPercentage / Quiz.GetTotalMarks();
        }

        public QuizResult GetResult()
        {
            return GetPercentage() >= Quiz.PassPercentage ? QuizResult.Pass : QuizResult.Fail;
        }

        public List<StudentAnswer> GetAnswers()
        {
            return new List<StudentAnswer>(_answers);
        }

        public string GetDetails()
        {
            string details = string.Format(DisplayFormats.AttemptFormat,
                Quiz.Name, Quiz.Course.Name, Quiz.DurationInMinutes,
                Score, Quiz.GetTotalMarks(), GetResult());

            for (int i = 0; i < _answers.Count; i++)
            {
                details += string.Format(DisplayFormats.AnswerFormat,
                    _answers[i].Question.GetDetails(),
                    _answers[i].Answer,
                    _answers[i].Question.GetCorrectAnswer());
            }
            return details;
        }
    }

    public class CourseManager
    {
        private List<Course> _courses = new List<Course>();

        public Course AddCourse(string name, string category,
            int numberOfLessons, int durationInHours, Admin admin)
        {
            Course course = new Course(name, category,
                numberOfLessons, durationInHours, admin);

            _courses.Add(course);

            return course;
        }

        public Course GetCourseById(int courseId)
        {
            for (int i = 0; i < _courses.Count; i++)
            {
                if (_courses[i].Id == courseId)
                    return _courses[i];
            }

            throw new ArgumentException(ErrorMessages.CourseNotFound);
        }

        public List<Course> GetAllCourses()
        {
            return new List<Course>(_courses);
        }

        public List<Course> GetAssignedCourses(Teacher teacher)
        {
            List<Course> assignedCourses = new List<Course>();

            for (int i = 0; i < _courses.Count; i++)
            {
                if (_courses[i].Teacher == teacher)
                    assignedCourses.Add(_courses[i]);
            }

            return assignedCourses;
        }

        public List<Course> GetUnassignedCourses()
        {
            List<Course> unassignedCourses = new List<Course>();

            for (int i = 0; i < _courses.Count; i++)
            {
                if (!_courses[i].HasTeacher)
                    unassignedCourses.Add(_courses[i]);
            }

            return unassignedCourses;
        }

        public List<Course> GetStudentCourses(Student student)
        {
            List<Course> studentCourses = new List<Course>();

            for (int i = 0; i < _courses.Count; i++)
            {
                if (_courses[i].IsEnrolled(student))
                    studentCourses.Add(_courses[i]);
            }

            return studentCourses;
        }

        public void AssignTeacher(int courseId, Teacher teacher)
        {
            Course course = GetCourseById(courseId);

            course.AssignTeacher(teacher);
        }

        public void ReleaseTeacher(int courseId, Teacher teacher)
        {
            Course course = GetCourseById(courseId);

            if (course.Teacher != teacher)
                throw new InvalidOperationException(ErrorMessages.TeacherNotAssigned);

            course.ReleaseTeacher();
        }

        public void EnrollStudent(int courseId, Student student)
        {
            Course course = GetCourseById(courseId);

            course.EnrollStudent(student);
        }
    }
    public class QuizManager
    {
        private List<Quiz> _quizzes = new List<Quiz>();
        private List<QuizAttempt> _attempts = new List<QuizAttempt>();

        public Quiz CreateQuiz(string name, int durationInMinutes,
            int passPercentage, Course course, Teacher teacher)
        {
            Quiz quiz = new Quiz(name, durationInMinutes,
                passPercentage, course, teacher);

            course.AddQuiz(quiz);
            _quizzes.Add(quiz);

            return quiz;
        }

        public Quiz GetQuizById(int quizId)
        {
            for (int i = 0; i < _quizzes.Count; i++)
            {
                if (_quizzes[i].Id == quizId)
                    return _quizzes[i];
            }

            throw new ArgumentException(ErrorMessages.QuizNotFound);
        }

        public List<Quiz> GetAllQuizzes()
        {
            return new List<Quiz>(_quizzes);
        }

        public List<Quiz> GetCourseQuizzes(Course course)
        {
            return course.GetQuizzes();
        }

        public void AddQuestion(Teacher teacher, int quizId,
            Question question)
        {
            Quiz quiz = GetQuizById(quizId);

            if (quiz.CreatedBy != teacher)
                throw new InvalidOperationException(
                    ErrorMessages.TeacherDidNotCreateQuiz);

            quiz.AddQuestion(question);
        }

        public void RemoveQuestion(Teacher teacher, int quizId,
            Question question)
        {
            Quiz quiz = GetQuizById(quizId);

            if (quiz.CreatedBy != teacher)
                throw new InvalidOperationException(
                    ErrorMessages.TeacherDidNotCreateQuiz);

            quiz.RemoveQuestion(question);
        }

        public QuizAttempt StartQuiz(Student student, int quizId)
        {
            Quiz quiz = GetQuizById(quizId);

            if (HasStudentTakenQuiz(student, quiz))
                throw new InvalidOperationException(
                    ErrorMessages.StudentAlreadyTookQuiz);

            QuizAttempt attempt = new QuizAttempt(student, quiz);

            _attempts.Add(attempt);

            return attempt;
        }

        public bool HasStudentTakenQuiz(Student student, Quiz quiz)
        {
            for (int i = 0; i < _attempts.Count; i++)
            {
                if (_attempts[i].Student == student &&
                    _attempts[i].Quiz == quiz)
                {
                    return true;
                }
            }

            return false;
        }

        public List<QuizAttempt> GetStudentAttempts(Student student)
        {
            List<QuizAttempt> studentAttempts =
                new List<QuizAttempt>();

            for (int i = 0; i < _attempts.Count; i++)
            {
                if (_attempts[i].Student == student)
                    studentAttempts.Add(_attempts[i]);
            }

            return studentAttempts;
        }

        public List<QuizAttempt> GetQuizAttempts(Quiz quiz)
        {
            List<QuizAttempt> quizAttempts =
                new List<QuizAttempt>();

            for (int i = 0; i < _attempts.Count; i++)
            {
                if (_attempts[i].Quiz == quiz)
                    quizAttempts.Add(_attempts[i]);
            }

            return quizAttempts;
        }
    }
    public static class MenuChoices
    {
        public const int Exit = 0;

        // main menu
        public const int AdminLogin = 1;
        public const int TeacherLogin = 2;
        public const int StudentLogin = 3;
        public const int Register = 4;

        // register menu
        public const int RegisterTeacher = 1;
        public const int RegisterStudent = 2;

        // admin menu
        public const int AddCourse = 1;
        public const int ViewAllCourses = 2;
        public const int ViewAllTeachers = 3;
        public const int ViewAllStudents = 4;

        // teacher menu
        public const int ViewAssignedCourses = 1;
        public const int ViewUnassignedCourses = 2;
        public const int AssignCourse = 3;
        public const int ReleaseCourse = 4;
        public const int CreateQuiz = 5;
        public const int AddQuestion = 6;
        public const int RemoveQuestion = 7;
        public const int ViewQuizDetails = 8;

        // student menu
        public const int StudentViewAllCourses = 1;
        public const int EnrollCourse = 2;
        public const int ViewEnrolledCourses = 3;
        public const int TakeQuiz = 4;
        public const int ViewTakenQuizzes = 5;
        public const int ViewAttemptDetails = 6;

        // question type menu
        public const int TrueOrFalseQuestion = 1;
        public const int MultipleChoiceQuestion = 2;
        public const int ShortAnswerQuestion = 3;
    }
    public static class ConsoleMessages
    {
        public const string MainMenu =
            "\n1. Admin Login" +
            "\n2. Teacher Login" +
            "\n3. Student Login" +
            "\n4. Register" +
            "\n0. Exit";

        public const string RegisterMenu =
            "\n1. Register Teacher" +
            "\n2. Register Student" +
            "\n0. Back";

        public const string AdminMenu =
            "\n1. Add Course" +
            "\n2. View All Courses" +
            "\n3. View All Teachers" +
            "\n4. View All Students" +
            "\n0. Logout";

        public const string TeacherMenu =
            "\n1. View Assigned Courses" +
            "\n2. View Unassigned Courses" +
            "\n3. Assign Myself To Course" +
            "\n4. Release Course" +
            "\n5. Create Quiz" +
            "\n6. Add Question To Quiz" +
            "\n7. Remove Question From Quiz" +
            "\n8. View Quiz Details" +
            "\n0. Logout";

        public const string StudentMenu =
            "\n1. View All Courses" +
            "\n2. Enroll In Course" +
            "\n3. View Enrolled Courses And Quizzes" +
            "\n4. Take Quiz" +
            "\n5. View Taken Quizzes" +
            "\n6. View Quiz Attempt Details" +
            "\n0. Logout";

        public const string QuestionTypeMenu =
            "\n1. True Or False" +
            "\n2. Multiple Choice" +
            "\n3. Short Answer";

        public const string EmailPrompt = "Email: ";
        public const string PasswordPrompt = "Password: ";
        public const string NamePrompt = "Name: ";
        public const string TitlePrompt = "Title: ";
        public const string GradePrompt = "Grade: ";
        public const string CourseIdPrompt = "Course ID: ";
        public const string QuizIdPrompt = "Quiz ID: ";
        public const string CourseNamePrompt = "Course Name: ";
        public const string CategoryPrompt = "Category: ";
        public const string LessonsPrompt = "Number Of Lessons: ";
        public const string CourseDurationPrompt = "Duration In Hours: ";
        public const string QuizNamePrompt = "Quiz Name: ";
        public const string QuizDurationPrompt = "Quiz Duration In Minutes: ";
        public const string PassPercentagePrompt = "Pass Percentage: ";
        public const string QuestionTextPrompt = "Question Text: ";
        public const string MarkPrompt = "Question Mark: ";
        public const string CorrectAnswerPrompt = "Correct Answer: ";
        public const string NumberOfOptionsPrompt = "Number Of Options: ";
        public const string CorrectOptionPrompt = "Correct Option Number: ";
        public const string AnswerPrompt = "Your Answer: ";
        public const string QuestionNumberPrompt = "Question Number: ";
        public const string AttemptNumberPrompt = "Attempt Number: ";

        public const string NoCourses = "No courses found";
        public const string NoTeachers = "No teachers found";
        public const string NoStudents = "No students found";
        public const string NoQuizzes = "No quizzes found";
        public const string NoAttempts = "No quizzes taken yet";
        public const string CourseAdded = "Course added successfully";
        public const string CourseAssigned = "Course assigned successfully";
        public const string CourseReleased = "Course released successfully";
        public const string EnrollmentCompleted = "Enrollment completed successfully";
        public const string RegistrationCompleted = "Registration completed successfully";
        public const string QuizCreated = "Quiz created successfully";
        public const string QuestionAdded = "Question added successfully";
        public const string QuestionRemoved = "Question removed successfully";
        public const string TimeIsUp = "Time is up. Quiz finished.";
        public const string PressEnter = "\nPress Enter to continue...";
        public const string TrueValue = "true";
        public const string FalseValue = "false";
    }
    public static class ConsoleInput
    {
        public static string ReadText(string message)
        {
            Console.Write(message);

            string text = Console.ReadLine();

            if (text == null)
                return string.Empty;

            return text;
        }

        public static int ReadNumber(string message)
        {
            int number;
            bool isValidNumber;

            do
            {
                Console.Write(message);

                isValidNumber = int.TryParse(Console.ReadLine(), out number);

                if (!isValidNumber)
                    Console.WriteLine(ErrorMessages.InvalidMenuChoice);

            } while (!isValidNumber);

            return number;
        }

        public static int ReadPositiveNumber(string message)
        {
            int number;

            do
            {
                number = ReadNumber(message);

                if (number < ValidationRules.MinDuration)
                    Console.WriteLine(ErrorMessages.InvalidDuration);

            } while (number < ValidationRules.MinDuration);

            return number;
        }

        public static bool ReadTrueOrFalse(string message)
        {
            string answer;

            do
            {
                answer = ReadText(message).Trim().ToLower();

                if (answer != ConsoleMessages.TrueValue &&
                    answer != ConsoleMessages.FalseValue)
                {
                    Console.WriteLine(ErrorMessages.TrueOrFalseOnly);
                }

            } while (answer != ConsoleMessages.TrueValue &&
                     answer != ConsoleMessages.FalseValue);

            return answer == ConsoleMessages.TrueValue;
        }

        public static void Pause()
        {
            Console.WriteLine(ConsoleMessages.PressEnter);
            Console.ReadLine();
        }
    }
    public class ConsoleApplication
    {
        private UserManager _userManager;
        private CourseManager _courseManager;
        private QuizManager _quizManager;

        public ConsoleApplication()
        {
            _userManager = new UserManager();
            _courseManager = new CourseManager();
            _quizManager = new QuizManager();
        }

        public void Run()
        {
            int choice;

            do
            {
                Console.WriteLine(ConsoleMessages.MainMenu);

                choice = ConsoleInput.ReadNumber("Choose: ");

                try
                {
                    if (choice == MenuChoices.AdminLogin)
                        Login();

                    else if (choice == MenuChoices.TeacherLogin)
                        Login();

                    else if (choice == MenuChoices.StudentLogin)
                        Login();

                    else if (choice == MenuChoices.Register)
                        Register();

                    else if (choice != MenuChoices.Exit)
                        Console.WriteLine(ErrorMessages.InvalidMenuChoice);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

            } while (choice != MenuChoices.Exit);
        }

        private void Register()
        {
            int choice;

            do
            {
                Console.WriteLine(ConsoleMessages.RegisterMenu);

                choice = ConsoleInput.ReadNumber("Choose: ");

                try
                {
                    if (choice == MenuChoices.RegisterTeacher)
                        RegisterTeacher();

                    else if (choice == MenuChoices.RegisterStudent)
                        RegisterStudent();

                    else if (choice != MenuChoices.Exit)
                        Console.WriteLine(ErrorMessages.InvalidMenuChoice);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                if (choice != MenuChoices.Exit)
                    ConsoleInput.Pause();

            } while (choice != MenuChoices.Exit);
        }

        private void RegisterTeacher()
        {
            string name = ConsoleInput.ReadText(ConsoleMessages.NamePrompt);
            string email = ConsoleInput.ReadText(ConsoleMessages.EmailPrompt);
            string password = ConsoleInput.ReadText(ConsoleMessages.PasswordPrompt);
            string title = ConsoleInput.ReadText(ConsoleMessages.TitlePrompt);

            _userManager.RegisterTeacher(name, email, password, title);

            Console.WriteLine(ConsoleMessages.RegistrationCompleted);
        }

        private void RegisterStudent()
        {
            string name = ConsoleInput.ReadText(ConsoleMessages.NamePrompt);
            string email = ConsoleInput.ReadText(ConsoleMessages.EmailPrompt);
            string password = ConsoleInput.ReadText(ConsoleMessages.PasswordPrompt);
            int grade = ConsoleInput.ReadPositiveNumber(ConsoleMessages.GradePrompt);

            _userManager.RegisterStudent(name, email, password, grade);

            Console.WriteLine(ConsoleMessages.RegistrationCompleted);
        }

        private void Login()
        {
            string email = ConsoleInput.ReadText(ConsoleMessages.EmailPrompt);
            string password = ConsoleInput.ReadText(ConsoleMessages.PasswordPrompt);

            User user = _userManager.Login(email, password);

            if (user == null)
            {
                Console.WriteLine(ErrorMessages.LoginFailed);
                ConsoleInput.Pause();
                return;
            }

            if (user.Role == UserRole.Admin)
                AdminMenu((Admin)user);

            else if (user.Role == UserRole.Teacher)
                TeacherMenu((Teacher)user);

            else if (user.Role == UserRole.Student)
                StudentMenu((Student)user);
        }

        private void AdminMenu(Admin admin)
        {
            int choice;

            do
            {
                Console.WriteLine(ConsoleMessages.AdminMenu);

                choice = ConsoleInput.ReadNumber("Choose: ");

                try
                {
                    if (choice == MenuChoices.AddCourse)
                        AddCourse(admin);

                    else if (choice == MenuChoices.ViewAllCourses)
                        ViewAllCoursesForAdmin();

                    else if (choice == MenuChoices.ViewAllTeachers)
                        ViewAllTeachers();

                    else if (choice == MenuChoices.ViewAllStudents)
                        ViewAllStudents();

                    else if (choice != MenuChoices.Exit)
                        Console.WriteLine(ErrorMessages.InvalidMenuChoice);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                if (choice != MenuChoices.Exit)
                    ConsoleInput.Pause();

            } while (choice != MenuChoices.Exit);
        }

        private void AddCourse(Admin admin)
        {
            string name = ConsoleInput.ReadText(ConsoleMessages.CourseNamePrompt);
            string category = ConsoleInput.ReadText(ConsoleMessages.CategoryPrompt);

            int numberOfLessons =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.LessonsPrompt);

            int durationInHours =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.CourseDurationPrompt);

            _courseManager.AddCourse(name, category,
                numberOfLessons, durationInHours, admin);

            Console.WriteLine(ConsoleMessages.CourseAdded);
        }

        private void ViewAllCoursesForAdmin()
        {
            List<Course> courses = _courseManager.GetAllCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoCourses);
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Course course = courses[i];

                Console.WriteLine("\nCourse ID: " + course.Id);
                Console.WriteLine(course.GetDetails());
                Console.WriteLine("Number of Students: " + course.StudentCount);

                DisplayQuizNames(course.GetQuizzes());
                DisplayStudentNames(course.GetStudents());
            }
        }

        private void ViewAllTeachers()
        {
            List<Teacher> teachers = _userManager.GetTeachers();

            if (teachers.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoTeachers);
                return;
            }

            for (int i = 0; i < teachers.Count; i++)
            {
                Teacher teacher = teachers[i];

                List<Course> courses =
                    _courseManager.GetAssignedCourses(teacher);

                int quizCount = GetTeacherQuizCount(courses);

                Console.WriteLine("\nTeacher Name: " + teacher.Name);
                Console.WriteLine("Email: " + teacher.Email);
                Console.WriteLine("Title: " + teacher.Title);
                Console.WriteLine("Number of Courses: " + courses.Count);
                Console.WriteLine("Number of Quizzes Created: " + quizCount);

                DisplayTeacherCourseNames(courses);
                DisplayTeacherQuizNames(courses);
            }
        }

        private void ViewAllStudents()
        {
            List<Student> students = _userManager.GetStudents();

            if (students.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoStudents);
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                Student student = students[i];

                List<Course> courses =
                    _courseManager.GetStudentCourses(student);

                List<QuizAttempt> attempts =
                    _quizManager.GetStudentAttempts(student);

                Console.WriteLine("\nStudent Name: " + student.Name);
                Console.WriteLine("Email: " + student.Email);
                Console.WriteLine("Grade: " + student.Grade);
                Console.WriteLine("Number of Courses: " + courses.Count);
                Console.WriteLine("Number of Quizzes Taken: " + attempts.Count);

                DisplayStudentCourseNames(courses);
                DisplayStudentQuizNames(attempts);
            }
        }

        private int GetTeacherQuizCount(List<Course> courses)
        {
            int quizCount = ValidationRules.NoMarks;

            for (int i = 0; i < courses.Count; i++)
            {
                quizCount += courses[i].QuizCount;
            }

            return quizCount;
        }

        private void TeacherMenu(Teacher teacher)
        {
            int choice;

            do
            {
                Console.WriteLine(ConsoleMessages.TeacherMenu);

                choice = ConsoleInput.ReadNumber("Choose: ");

                try
                {
                    if (choice == MenuChoices.ViewAssignedCourses)
                        ViewAssignedCourses(teacher);

                    else if (choice == MenuChoices.ViewUnassignedCourses)
                        ViewUnassignedCourses();

                    else if (choice == MenuChoices.AssignCourse)
                        AssignCourse(teacher);

                    else if (choice == MenuChoices.ReleaseCourse)
                        ReleaseCourse(teacher);

                    else if (choice == MenuChoices.CreateQuiz)
                        CreateQuiz(teacher);

                    else if (choice == MenuChoices.AddQuestion)
                        AddQuestion(teacher);

                    else if (choice == MenuChoices.RemoveQuestion)
                        RemoveQuestion(teacher);

                    else if (choice == MenuChoices.ViewQuizDetails)
                        ViewQuizDetails(teacher);

                    else if (choice != MenuChoices.Exit)
                        Console.WriteLine(ErrorMessages.InvalidMenuChoice);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                if (choice != MenuChoices.Exit)
                    ConsoleInput.Pause();

            } while (choice != MenuChoices.Exit);
        }

        private void ViewAssignedCourses(Teacher teacher)
        {
            List<Course> courses = _courseManager.GetAssignedCourses(teacher);

            if (courses.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoCourses);
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine("\nCourse ID: " + courses[i].Id);
                Console.WriteLine(courses[i].GetDetails());
            }
        }

        private void ViewUnassignedCourses()
        {
            List<Course> courses = _courseManager.GetUnassignedCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoCourses);
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine("\nCourse ID: " + courses[i].Id);
                Console.WriteLine(courses[i].GetBasicDetails());
            }
        }

        private void AssignCourse(Teacher teacher)
        {
            int courseId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.CourseIdPrompt);

            _courseManager.AssignTeacher(courseId, teacher);

            Console.WriteLine(ConsoleMessages.CourseAssigned);
        }

        private void ReleaseCourse(Teacher teacher)
        {
            int courseId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.CourseIdPrompt);

            _courseManager.ReleaseTeacher(courseId, teacher);

            Console.WriteLine(ConsoleMessages.CourseReleased);
        }

        private void CreateQuiz(Teacher teacher)
        {
            int courseId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.CourseIdPrompt);

            Course course = _courseManager.GetCourseById(courseId);

            string name = ConsoleInput.ReadText(ConsoleMessages.QuizNamePrompt);

            int duration =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.QuizDurationPrompt);

            int passPercentage =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.PassPercentagePrompt);

            Quiz quiz = _quizManager.CreateQuiz(name, duration,
                passPercentage, course, teacher);

            Console.WriteLine(ConsoleMessages.QuizCreated);
            Console.WriteLine("Quiz ID: " + quiz.Id);
        }

        private void AddQuestion(Teacher teacher)
        {
            int quizId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.QuizIdPrompt);

            Question question = CreateQuestion();

            _quizManager.AddQuestion(teacher, quizId, question);

            Console.WriteLine(ConsoleMessages.QuestionAdded);
        }

        private Question CreateQuestion()
        {
            Console.WriteLine(ConsoleMessages.QuestionTypeMenu);

            int type = ConsoleInput.ReadNumber("Choose Question Type: ");

            string text =
                ConsoleInput.ReadText(ConsoleMessages.QuestionTextPrompt);

            int mark =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.MarkPrompt);

            if (type == MenuChoices.TrueOrFalseQuestion)
            {
                bool correctAnswer = ConsoleInput.ReadTrueOrFalse(
                    ConsoleMessages.CorrectAnswerPrompt);

                return new TrueOrFalseQuestion(text, mark, correctAnswer);
            }

            if (type == MenuChoices.MultipleChoiceQuestion)
            {
                return CreateMultipleChoiceQuestion(text, mark);
            }

            if (type == MenuChoices.ShortAnswerQuestion)
            {
                string correctAnswer =
                    ConsoleInput.ReadText(ConsoleMessages.CorrectAnswerPrompt);

                return new ShortAnswerQuestion(text, mark, correctAnswer);
            }

            throw new ArgumentException(ErrorMessages.InvalidMenuChoice);
        }

        private MultipleChoiceQuestion CreateMultipleChoiceQuestion(
            string text, int mark)
        {
            int numberOfOptions =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.NumberOfOptionsPrompt);

            List<string> options = new List<string>();

            for (int i = 0; i < numberOfOptions; i++)
            {
                string option =
                    ConsoleInput.ReadText("Option " +
                    (i + ValidationRules.FirstOptionNumber) + ": ");

                options.Add(option);
            }

            int correctOptionNumber =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.CorrectOptionPrompt);

            return new MultipleChoiceQuestion(text, mark,
                options, correctOptionNumber);
        }

        private void RemoveQuestion(Teacher teacher)
        {
            int quizId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.QuizIdPrompt);

            Quiz quiz = _quizManager.GetQuizById(quizId);

            int questionNumber =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.QuestionNumberPrompt);

            if (questionNumber < ValidationRules.FirstOptionNumber ||
                questionNumber > quiz.QuestionCount)
            {
                throw new ArgumentException(
                    ErrorMessages.InvalidQuestionNumber);
            }

            Question question =
                quiz[questionNumber - ValidationRules.FirstOptionNumber];

            _quizManager.RemoveQuestion(teacher, quizId, question);

            Console.WriteLine(ConsoleMessages.QuestionRemoved);
        }

        private void ViewQuizDetails(Teacher teacher)
        {
            int quizId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.QuizIdPrompt);

            Quiz quiz = _quizManager.GetQuizById(quizId);

            if (quiz.CreatedBy != teacher)
            {
                throw new InvalidOperationException(
                    ErrorMessages.TeacherDidNotCreateQuiz);
            }

            DisplayQuizDetails(quiz);
        }

        private void StudentMenu(Student student)
        {
            int choice;

            do
            {
                Console.WriteLine(ConsoleMessages.StudentMenu);

                choice = ConsoleInput.ReadNumber("Choose: ");

                try
                {
                    if (choice == MenuChoices.StudentViewAllCourses)
                        ViewAllCoursesForStudent();

                    else if (choice == MenuChoices.EnrollCourse)
                        EnrollCourse(student);

                    else if (choice == MenuChoices.ViewEnrolledCourses)
                        ViewEnrolledCourses(student);

                    else if (choice == MenuChoices.TakeQuiz)
                        TakeQuiz(student);

                    else if (choice == MenuChoices.ViewTakenQuizzes)
                        ViewTakenQuizzes(student);

                    else if (choice == MenuChoices.ViewAttemptDetails)
                        ViewAttemptDetails(student);

                    else if (choice != MenuChoices.Exit)
                        Console.WriteLine(ErrorMessages.InvalidMenuChoice);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }

                if (choice != MenuChoices.Exit)
                    ConsoleInput.Pause();

            } while (choice != MenuChoices.Exit);
        }

        private void ViewAllCoursesForStudent()
        {
            List<Course> courses = _courseManager.GetAllCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoCourses);
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Course course = courses[i];

                Console.WriteLine("\nCourse ID: " + course.Id);
                Console.WriteLine(course.GetDetails());
                DisplayQuizNames(course.GetQuizzes());
            }
        }

        private void EnrollCourse(Student student)
        {
            int courseId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.CourseIdPrompt);

            _courseManager.EnrollStudent(courseId, student);

            Console.WriteLine(ConsoleMessages.EnrollmentCompleted);
        }

        private void ViewEnrolledCourses(Student student)
        {
            List<Course> courses = _courseManager.GetStudentCourses(student);

            if (courses.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoCourses);
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine("\nCourse ID: " + courses[i].Id);
                Console.WriteLine(courses[i].GetDetails());
                DisplayQuizNames(courses[i].GetQuizzes());
            }
        }

        private void TakeQuiz(Student student)
        {
            int quizId =
                ConsoleInput.ReadPositiveNumber(ConsoleMessages.QuizIdPrompt);

            QuizAttempt attempt = _quizManager.StartQuiz(student, quizId);

            Console.WriteLine("Quiz started.");
            Console.WriteLine("You have " +
                attempt.Quiz.DurationInMinutes + " minutes.");

            while (!attempt.IsFinished)
            {
                Question question = attempt.GetCurrentQuestion();

                Console.WriteLine("\n" + question.GetDetails());

                string answer =
                    ConsoleInput.ReadText(ConsoleMessages.AnswerPrompt);

                bool answerWasAccepted = attempt.SubmitAnswer(answer);

                if (!answerWasAccepted)
                    Console.WriteLine(ConsoleMessages.TimeIsUp);
            }

            Console.WriteLine("\nQuiz finished.");
            Console.WriteLine("Score: " + attempt.Score +
                "/" + attempt.Quiz.GetTotalMarks());

            Console.WriteLine("Result: " + attempt.GetResult());
        }

        private void ViewTakenQuizzes(Student student)
        {
            List<QuizAttempt> attempts =
                _quizManager.GetStudentAttempts(student);

            if (attempts.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoAttempts);
                return;
            }

            for (int i = 0; i < attempts.Count; i++)
            {
                Console.WriteLine("\nAttempt Number: " +
                    (i + ValidationRules.FirstOptionNumber));

                Console.WriteLine(attempts[i].GetDetails());
            }
        }

        private void ViewAttemptDetails(Student student)
        {
            List<QuizAttempt> attempts =
                _quizManager.GetStudentAttempts(student);

            if (attempts.Count == 0)
            {
                Console.WriteLine(ConsoleMessages.NoAttempts);
                return;
            }

            int attemptNumber =
                ConsoleInput.ReadPositiveNumber(
                    ConsoleMessages.AttemptNumberPrompt);

            if (attemptNumber < ValidationRules.FirstOptionNumber ||
                attemptNumber > attempts.Count)
            {
                throw new ArgumentException(
                    ErrorMessages.InvalidQuestionNumber);
            }

            QuizAttempt attempt =
                attempts[attemptNumber - ValidationRules.FirstOptionNumber];

            Console.WriteLine(attempt.GetDetails());
        }

        private void DisplayQuizDetails(Quiz quiz)
        {
            Console.WriteLine("\nQuiz ID: " + quiz.Id);
            Console.WriteLine(quiz.GetDetails());

            for (int i = 0; i < quiz.QuestionCount; i++)
            {
                Console.WriteLine("\nQuestion " +
                    (i + ValidationRules.FirstOptionNumber));

                Console.WriteLine(quiz[i].GetDetails());
            }
        }

        private void DisplayQuizNames(List<Quiz> quizzes)
        {
            Console.WriteLine("Quiz Names:");

            if (quizzes.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            for (int i = 0; i < quizzes.Count; i++)
            {
                Console.WriteLine("- " + quizzes[i].Name +
                    " (ID: " + quizzes[i].Id + ")");
            }
        }

        private void DisplayStudentNames(List<Student> students)
        {
            Console.WriteLine("Student Names:");

            if (students.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine("- " + students[i].Name);
            }
        }

        private void DisplayTeacherCourseNames(List<Course> courses)
        {
            Console.WriteLine("Course Names With Categories:");

            if (courses.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine("- " + courses[i].Name +
                    " (" + courses[i].Category + ")");
            }
        }

        private void DisplayTeacherQuizNames(List<Course> courses)
        {
            Console.WriteLine("Quiz Names:");

            bool hasQuiz = false;

            for (int i = 0; i < courses.Count; i++)
            {
                List<Quiz> quizzes = courses[i].GetQuizzes();

                for (int j = 0; j < quizzes.Count; j++)
                {
                    Console.WriteLine("- " + quizzes[j].Name);
                    hasQuiz = true;
                }
            }

            if (!hasQuiz)
                Console.WriteLine("None");
        }

        private void DisplayStudentCourseNames(List<Course> courses)
        {
            Console.WriteLine("Course Names:");

            if (courses.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            for (int i = 0; i < courses.Count; i++)
            {
                Console.WriteLine("- " + courses[i].Name);
            }
        }

        private void DisplayStudentQuizNames(
            List<QuizAttempt> attempts)
        {
            Console.WriteLine("Quiz Names:");

            if (attempts.Count == 0)
            {
                Console.WriteLine("None");
                return;
            }

            for (int i = 0; i < attempts.Count; i++)
            {
                Console.WriteLine("- " + attempts[i].Quiz.Name);
            }
        }
    }
    public class Program
    {
        public static void Main()
        {
            ConsoleApplication application =
                new ConsoleApplication();

            application.Run();
        }
    }
}
