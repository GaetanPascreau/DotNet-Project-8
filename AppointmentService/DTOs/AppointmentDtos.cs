﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AppointmentService.DTOs
{
    public record AppointmentDto(int Id, DateTime StartDateTime, DateTime EndDateTime, int ConsultantId, string PatientId);

    public record CreateAppointmentDto([Required][MyStartDateTime(ErrorMessage = "invalid date")] DateTime StartDateTime,
                                       [Range(1, int.MaxValue, ErrorMessage = "ConsultantId is required")] int ConsultantId,
                                       string PatientId);

    public record UpdateAppointmentDto([Required][MyStartDateTime(ErrorMessage = "invalid date")] DateTime StartDateTime,
                                       [Range(1, int.MaxValue, ErrorMessage = "ConsultantId is required")] int ConsultantId,
                                       string PatientId);

    
    // Add a validation for StartDatetime which cannot occur before current DateTime
    public class MyStartDateTime : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime d = Convert.ToDateTime(value);
            return d >= DateTime.UtcNow;
        }
    }

}