package com.campushire.service;
import org.springframework.stereotype.Service;
import com.campushire.dto.*;
import com.campushire.entity.*;
import com.campushire.repository.*;

@Service
public class SocialLinkService{

    private final SocialLinkRepository repository;
    private final StudentRepository studentRepository;

    public SocialLinkService(SocialLinkRepository repository,StudentRepository studentRepository){
        this.repository=repository;
        this.studentRepository=studentRepository;
    }

    public SocialLinkResponse save(SocialLinkRequest request){
        Student student=studentRepository.findById(request.getStudentId()).orElseThrow();
        SocialLink link=SocialLink.builder()
                .student(student)
                .linkedin(request.getLinkedin())
                .github(request.getGithub())
                .portfolio(request.getPortfolio())
                .leetcode(request.getLeetcode())
                .hackerrank(request.getHackerrank())
                .build();
        return map(repository.save(link));
    }

    public SocialLinkResponse get(Integer studentId){
        return map(repository.findByStudentId(studentId));
    }

    private SocialLinkResponse map(SocialLink s){
        return SocialLinkResponse.builder()
                .id(s.getId())
                .linkedin(s.getLinkedin())
                .github(s.getGithub())
                .portfolio(s.getPortfolio())
                .leetcode(s.getLeetcode())
                .hackerrank(s.getHackerrank())
                .build();
    }
}