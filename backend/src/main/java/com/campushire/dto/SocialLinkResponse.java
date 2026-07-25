package com.campushire.dto;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class SocialLinkResponse{
    private Long id;
    private String linkedin;
    private String github;
    private String portfolio;
    private String leetcode;
    private String hackerrank;
}