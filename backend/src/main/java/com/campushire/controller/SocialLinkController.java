package com.campushire.controller;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.*;
import com.campushire.service.SocialLinkService;

@RestController
@RequestMapping("/api/social-links")
public class SocialLinkController{

    private final SocialLinkService service;

    public SocialLinkController(SocialLinkService service){
        this.service=service;
    }

    @PostMapping
    public SocialLinkResponse save(@RequestBody SocialLinkRequest request){
        return service.save(request);
    }

    @GetMapping("/{studentId}")
    public SocialLinkResponse get(@PathVariable Integer studentId){
        return service.get(studentId);
    }
}