package com.campushire.dto;

import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class SkillResponse {
    private Long id;
    private String skillName;
    private String proficiency;
}