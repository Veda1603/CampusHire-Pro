package com.campushire.dto;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class ProjectRequest{
    private Integer studentId;
    private String title;
    private String description;
    private String technologiesUsed;
    private String githubLink;
    private String liveDemoLink;
    private String projectImageUrl;
}