from utils.skill_loader import load_skills

SKILLS = load_skills()


def extract_skills(text):

    found = set()

    lower = text.lower()

    for skill in SKILLS:

        if skill.lower() in lower:
            found.add(skill)

    return sorted(found)