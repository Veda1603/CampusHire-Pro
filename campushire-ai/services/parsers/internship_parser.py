import re


def extract_internships(lines):

    internships = []

    if not lines:
        return internships

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

        if "intern" in line.lower():

            if current["designation"]:
                internships.append(current)

            current = {
                "company": "",
                "designation": line,
                "duration": "",
                "description": ""
            }

            year = re.search(
                r"(20\d{2}).*(20\d{2}|Present|Current)",
                line,
                re.I
            )

            if year:
                current["duration"] = year.group()

        elif current["company"] == "":

            current["company"] = line

        else:

            if current["description"]:
                current["description"] += " "

            current["description"] += line

    if current["designation"]:
        internships.append(current)

    return internships