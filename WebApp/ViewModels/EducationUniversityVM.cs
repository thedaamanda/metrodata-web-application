using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
	public class EducationUniversityVM
	{
		public int Id { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public double GPA { get; set; }
        public string UniversityName { get; set; }
    }
}
