from services.parser_service import split_sections

from services.parsers.profile_parser import extract_profile
from services.parsers.skill_parser import extract_skills
from services.parsers.education_parser import extract_education
from services.parsers.project_parser import extract_projects
from services.parsers.internship_parser import extract_internships
from services.parsers.experience_parser import extract_experience
from services.parsers.certification_parser import extract_certifications
from services.parsers.post_processor import post_process

from utils.resume_cleaner import clean_resume


def parse_resume(text):

    # Clean OCR and formatting issues
    text = clean_resume(text)

    # Split cleaned resume into sections
    sections = split_sections(text)

    # Extract profile details
    profile = extract_profile(text)

    summary = " ".join(
        sections.get("SUMMARY", [])
    ).strip()

    skills_text = "\n".join(
        sections.get("SKILLS", [])
    )

    result = {

        "name": profile["name"],

        "email": profile["email"],

        "phone": profile["phone"],

        "linkedin": profile["linkedin"],

        "github": profile["github"],

        "summary": summary,

        "skills": extract_skills(skills_text),

        "education": extract_education(
            sections.get("EDUCATION", [])
        ),

        "projects": extract_projects(
            sections.get("PROJECTS", [])
        ),

        "internships": extract_internships(
            sections.get("INTERNSHIPS", [])
        ),

        "experience": extract_experience(
            sections.get("EXPERIENCE", [])
        ),

        "certifications": extract_certifications(
            sections.get("CERTIFICATIONS", [])
        )

    }

    return post_process(result)