package com.campushire.dto;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.NotNull;
import lombok.Getter;
import lombok.Setter;
@Getter
@Setter
public class RecruiterRequest {
    @NotNull(message="Company id is required")
    private Long companyId;
    @NotBlank(message="Designation is required")
    private String designation;
    @NotBlank(message="Phone number is required")
    private String phoneNumber;
}