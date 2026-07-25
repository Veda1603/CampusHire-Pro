package com.campushire.controller;

import java.util.List;
import org.springframework.web.bind.annotation.*;
import com.campushire.dto.*;
import com.campushire.service.DocumentService;

@RestController
@RequestMapping("/api/documents")
public class DocumentController {

    private final DocumentService service;

    public DocumentController(DocumentService service){
        this.service=service;
    }

    @PostMapping
    public DocumentResponse add(@RequestBody DocumentRequest request){
        return service.add(request);
    }

    @GetMapping("/{studentId}")
    public List<DocumentResponse> get(@PathVariable Integer studentId){
        return service.get(studentId);
    }
}