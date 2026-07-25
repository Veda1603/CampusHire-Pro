package com.campushire.dto;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class SkillRequest {
    private Integer studentId;
    private String skillName;
    private String proficiency;
}