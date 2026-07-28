import re

from utils.skill_loader import load_skills

SKILLS = load_skills()


SECTION_HEADERS = {
    "SUMMARY",
    "EDUCATION",
    "SKILLS",
    "EXPERIENCE",
    "INTERNSHIP",
    "CERTIFICATIONS"
}


def extract_technologies(text):

    found = []

    lower = text.lower()

    for skill in SKILLS:

        if skill.lower() in lower:

            found.append(skill)

    return sorted(set(found))


def is_project_title(line):

    if line.startswith(("•", "-", "*")):
        return False

    if line.upper() in SECTION_HEADERS:
        return False

    if line.endswith("."):
        return False

    if len(line.split()) > 8:
        return False

    return True


def extract_projects(lines):

    projects = []

    current = None

    for line in lines:

        line = line.strip()

        if not line:
            continue

        if is_project_title(line):

            if current:

                current["description"] = current["description"].strip()

                current["technologies"] = extract_technologies(
                    current["description"]
                )

                projects.append(current)

            current = {
                "title": line,
                "description": "",
                "technologies": []
            }

        else:

            if current:

                line = re.sub(r"^[•*-]\s*", "", line)

                current["description"] += " " + line

    if current:

        current["description"] = current["description"].strip()

        current["technologies"] = extract_technologies(
            current["description"]
        )

        projects.append(current)

    return projects