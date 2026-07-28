import re


def extract_experience(lines):

    experiences = []

    if not lines:
        return experiences

    current = {
        "company": "",
        "designation": "",
        "duration": "",
        "description": ""
    }

    for line in lines:

        line = line.strip()

        if not line:
            continue

        if re.search(r"(20\d{2}|19\d{2})", line):

            if current["designation"]:
                experiences.append(current)

            current = {
                "company": "",
                "designation": line,
                "duration": line,
                "description": ""
            }

        elif current["company"] == "":

            current["company"] = line

        else:

            if current["description"]:
                current["description"] += " "

            current["description"] += line

    if current["designation"]:
        experiences.append(current)

    return experiences