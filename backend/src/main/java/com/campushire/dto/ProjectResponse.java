package com.campushire.dto;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class ProjectResponse{
    private Long id;
    private String title;
    private String description;
    private String technologiesUsed;
    private String githubLink;
    private String liveDemoLink;
    private String projectImageUrl;
}