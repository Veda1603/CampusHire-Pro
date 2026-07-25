-- ============================================================================
-- CampusHire Pro: PostgreSQL Relational Database Schema
-- File: infrastructure/postgres/schema.sql
-- ============================================================================

-- Create Custom Enum Types
CREATE TYPE user_role AS ENUM (
    'STUDENT',
    'RECRUITER',
    'ADMIN'
);
CREATE TYPE job_type AS ENUM ('FULL_TIME', 'PART_TIME', 'INTERNSHIP');
CREATE TYPE application_status AS ENUM ('APPLIED', 'SHORTLISTED', 'ASSESSMENT_SCHEDULED', 'INTERVIEWED', 'ACCEPTED', 'REJECTED');

-- ----------------------------------------------------------------------------
-- Table: users (Core Authentication & Role Management)
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) UNIQUE NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(150) NOT NULL,
    role user_role NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ----------------------------------------------------------------------------
-- Table: students (Student Academic & Profile Data)
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS students (
    id SERIAL PRIMARY KEY,
    user_id INT UNIQUE NOT NULL REFERENCES users(id) ON DELETE CASCADE,
    roll_number VARCHAR(50) UNIQUE NOT NULL,
    department VARCHAR(100) NOT NULL,
    cgpa NUMERIC(3, 2) CHECK (cgpa >= 0.0 AND cgpa <= 10.0),
    graduation_year INT NOT NULL,
    resume_url TEXT,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ----------------------------------------------------------------------------
-- Table: companies
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS companies (
    id SERIAL PRIMARY KEY,
    company_name VARCHAR(200) UNIQUE NOT NULL,
    company_email VARCHAR(255) UNIQUE,
    company_website VARCHAR(255),
    industry VARCHAR(100),
    location VARCHAR(150),
    description TEXT,
    logo_url TEXT,
    is_verified BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ----------------------------------------------------------------------------
-- Table: recruiters
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS recruiters (
    id SERIAL PRIMARY KEY,
    user_id INT UNIQUE NOT NULL
        REFERENCES users(id)
        ON DELETE CASCADE,
    company_id INT NOT NULL
        REFERENCES companies(id)
        ON DELETE CASCADE,
    designation VARCHAR(100),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ----------------------------------------------------------------------------
-- Table: jobs (Job Postings Engine)
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS jobs (
    id SERIAL PRIMARY KEY,
   company_id INT NOT NULL
    REFERENCES companies(id)
    ON DELETE CASCADE,
recruiter_id INT NOT NULL
    REFERENCES recruiters(id)
    ON DELETE CASCADE,
    title VARCHAR(200) NOT NULL,
    description TEXT NOT NULL,
    location VARCHAR(100) NOT NULL,
    salary_package VARCHAR(100),
    type job_type NOT NULL DEFAULT 'FULL_TIME',
    min_cgpa NUMERIC(3, 2) DEFAULT 0.0,
    is_open BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- ----------------------------------------------------------------------------
-- Table: applications (Job Application Pipeline)
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS applications (
    id SERIAL PRIMARY KEY,
    job_id INT NOT NULL REFERENCES jobs(id) ON DELETE CASCADE,
    student_id INT NOT NULL REFERENCES students(id) ON DELETE CASCADE,
    status application_status NOT NULL DEFAULT 'APPLIED',
    applied_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(job_id, student_id)
);

-- ----------------------------------------------------------------------------
-- Table: placement_drives (Placement Drives Managed by Officers)
-- ----------------------------------------------------------------------------
CREATE TABLE IF NOT EXISTS placement_drives (
    id SERIAL PRIMARY KEY,
    title VARCHAR(200) NOT NULL,
    description TEXT,
    drive_date DATE NOT NULL,
    location VARCHAR(150),
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP
);

-- Create Indexes for Performance Optimization
CREATE INDEX idx_users_email ON users(email);
CREATE INDEX idx_jobs_company ON jobs(company_id);
CREATE INDEX idx_jobs_recruiter ON jobs(recruiter_id);
CREATE INDEX idx_applications_job ON applications(job_id);
CREATE INDEX idx_applications_student ON applications(student_id);