using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using GlaucomaWay.Users;

namespace GlaucomaWay.Models
{
    public class PatientModel
    {
        [Key]
        public int Id { get; set; }

        public DateTime BithDate { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [JsonIgnore]
        public ICollection<Vf14ResultModel> Results { get; set; }

        [NotMapped]
        public int Age
            => (int)(DateTime.Now - BithDate).TotalDays / 365;
    }

    public class PatientCreateOrUpdateModel
    {
        public DateTime BithDate { get; set; }

        public PatientModel ToPatientModel(string userId)
            => new () { BithDate = BithDate, UserId = userId };
    }
}
