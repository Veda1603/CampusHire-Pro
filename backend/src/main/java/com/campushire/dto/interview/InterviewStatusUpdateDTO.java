package com.campushire.dto.interview;

import com.campushire.entity.InterviewStatus;

import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class InterviewStatusUpdateDTO {
    private InterviewStatus status;
}