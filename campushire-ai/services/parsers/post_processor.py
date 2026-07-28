import re


def merge_multiline_projects(projects):

    merged = []

    for project in projects:

        description = project.get("description", "")

        description = re.sub(r"\s+", " ", description).strip()

        project["description"] = description

        merged.append(project)

    return merged


def remove_duplicate_skills(skills):

    seen = set()

    cleaned = []

    for skill in skills:

        key = skill.lower()

        if key not in seen:

            cleaned.append(skill)

            seen.add(key)

    return cleaned


def clean_links(profile):

    for key in ["linkedin", "github"]:

        if profile.get(key):

            url = profile[key].strip()

            url = url.rstrip(".,);")

            profile[key] = url

    return profile


def post_process(result):

    result["projects"] = merge_multiline_projects(
        result.get("projects", [])
    )

    result["skills"] = remove_duplicate_skills(
        result.get("skills", [])
    )

    profile = {

        "linkedin": result.get("linkedin", ""),

        "github": result.get("github", "")

    }

    profile = clean_links(profile)

    result["linkedin"] = profile["linkedin"]

    result["github"] = profile["github"]

    return result