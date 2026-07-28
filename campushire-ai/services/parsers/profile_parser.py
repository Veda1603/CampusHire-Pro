import re


def extract_name(text):

    lines = [line.strip() for line in text.split("\n") if line.strip()]

    ignore = [
        "resume",
        "curriculum vitae",
        "email",
        "phone",
        "contact",
        "linkedin",
        "github"
    ]

    for line in lines[:8]:

        if any(word in line.lower() for word in ignore):
            continue

        if any(ch.isdigit() for ch in line):
            continue

        words = line.split()

        if 2 <= len(words) <= 5:
            return line.title()

    return ""


def extract_email(text):

    match = re.search(
        r"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}",
        text
    )

    return match.group(0) if match else ""


def extract_phone(text):

    match = re.search(
        r"(?:\+91[\-\s]?)?[6-9]\d{9}",
        text
    )

    return match.group(0) if match else ""


def extract_linkedin(text):

    patterns = [

        r"https?://(?:www\.)?linkedin\.com/[^\s|]+",

        r"(?:www\.)?linkedin\.com/[^\s|]+"

    ]

    for pattern in patterns:

        match = re.search(pattern, text, re.I)

        if match:

            url = match.group(0)

            if not url.startswith("http"):
                url = "https://" + url

            return url

    return ""


def extract_github(text):

    patterns = [

        r"https?://(?:www\.)?github\.com/[^\s|]+",

        r"(?:www\.)?github\.com/[^\s|]+"

    ]

    for pattern in patterns:

        match = re.search(pattern, text, re.I)

        if match:

            url = match.group(0)

            if not url.startswith("http"):
                url = "https://" + url

            return url

    return ""


def extract_profile(text):

    return {

        "name": extract_name(text),

        "email": extract_email(text),

        "phone": extract_phone(text),

        "linkedin": extract_linkedin(text),

        "github": extract_github(text)

    }