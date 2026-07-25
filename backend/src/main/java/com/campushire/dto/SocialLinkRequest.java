package com.campushire.dto;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class SocialLinkRequest{
    private Integer studentId;
    private String linkedin;
    private String github;
    private String portfolio;
    private String leetcode;
    private String hackerrank;
}