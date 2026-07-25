package com.campushire.controller;

import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/api/test")
public class TestController {

    @GetMapping("/secure")
    public String secureTest() {
        return "JWT Authentication Working!";
    }
}