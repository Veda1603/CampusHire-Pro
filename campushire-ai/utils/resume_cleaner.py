import re


SECTION_HEADERS = [
    "SUMMARY",
    "PROFILE",
    "OBJECTIVE",
    "EDUCATION",
    "ACADEMICS",
    "SKILLS",
    "TECHNICAL SKILLS",
    "PROJECT",
    "PROJECTS",
    "EXPERIENCE",
    "WORK EXPERIENCE",
    "INTERNSHIP",
    "INTERNSHIPS",
    "INTERSHIP",
    "CERTIFICATION",
    "CERTIFICATIONS",
    "ACHIEVEMENTS",
    "LANGUAGES"
]


OCR_FIXES = {

    "INTERSHIP": "INTERNSHIP",

    "CERTIFICATIONS": "CERTIFICATIONS",

    "TECHNICALSKILLS": "TECHNICAL SKILLS",

    "WORKEXPERIENCE": "WORK EXPERIENCE",

    "ACADEMICQUALIFICATION": "ACADEMIC QUALIFICATION"
}


def clean_resume(text):

    lines = []

    for line in text.split("\n"):

        line = line.strip()

        if not line:
            continue

        line = re.sub(r"\s+", " ", line)


        for wrong, correct in OCR_FIXES.items():

            line = line.replace(wrong, correct)


        # Additional OCR spelling fixes
        line = line.replace(
            "Istitute",
            "Institute"
        )


        lines.append(line)

    return "\n".join(lines)

    lines = []

    for line in text.split("\n"):

        line = line.strip()

        if not line:
            continue

        line = re.sub(r"\s+", " ", line)

        for wrong, correct in OCR_FIXES.items():

            line = line.replace(wrong, correct)

        lines.append(line)

    return "\n".join(lines)