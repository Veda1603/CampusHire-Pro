import re

SECTION_ALIASES = {

    "SUMMARY": "SUMMARY",
    "PROFILE": "SUMMARY",
    "PROFESSIONAL SUMMARY": "SUMMARY",
    "OBJECTIVE": "SUMMARY",
    "CAREER OBJECTIVE": "SUMMARY",

    "EDUCATION": "EDUCATION",
    "ACADEMICS": "EDUCATION",
    "ACADEMIC": "EDUCATION",
    "ACADEMIC QUALIFICATION": "EDUCATION",
    "QUALIFICATION": "EDUCATION",

    "SKILLS": "SKILLS",
    "TECHNICAL SKILLS": "SKILLS",
    "TECHNICAL EXPERTISE": "SKILLS",
    "TECH STACK": "SKILLS",
    "TOOLS": "SKILLS",
    "CORE COMPETENCIES": "SKILLS",

    "PROJECT": "PROJECTS",
    "PROJECTS": "PROJECTS",

    "EXPERIENCE": "EXPERIENCE",
    "WORK EXPERIENCE": "EXPERIENCE",
    "WORK HISTORY": "EXPERIENCE",
    "EMPLOYMENT": "EXPERIENCE",

    "INTERNSHIP": "INTERNSHIPS",
    "INTERNSHIPS": "INTERNSHIPS",
    "INTERSHIP": "INTERNSHIPS",
    "SUMMER INTERNSHIP": "INTERNSHIPS",

    "CERTIFICATION": "CERTIFICATIONS",
    "CERTIFICATIONS": "CERTIFICATIONS",
    "CERTIFICATE": "CERTIFICATIONS",
    "CERTIFICATES": "CERTIFICATIONS",

    "ACHIEVEMENTS": "ACHIEVEMENTS",
    "LANGUAGES": "LANGUAGES"
}


def split_sections(text):

    sections = {}

    current_section = "GENERAL"

    sections[current_section] = []

    for line in text.split("\n"):

        line = line.strip()

        if not line:
            continue

        normalized = re.sub(r"\s+", " ", line).upper()

        if normalized in SECTION_ALIASES:

            current_section = SECTION_ALIASES[normalized]

            if current_section not in sections:
                sections[current_section] = []

        else:

            sections.setdefault(current_section, []).append(line)

    return sections