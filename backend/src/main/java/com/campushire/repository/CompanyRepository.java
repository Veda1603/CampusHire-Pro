package com.campushire.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import com.campushire.entity.Company;

public interface CompanyRepository extends JpaRepository<Company, Long> {
}