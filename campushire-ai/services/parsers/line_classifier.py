import re

DEGREE_KEYWORDS = [
    "b.e",
    "be",
    "b.tech",
    "bachelor",
    "master",
    "m.tech",
    "mca",
    "bca",
    "diploma",
    "ssc",
    "hsc",
    "secondary",
    "higher secondary"
]

INSTITUTION_KEYWORDS = [
    "college",
    "university",
    "polytechnic",
    "school",
    "institute",
    "academy"
]

PROJECT_KEYWORDS = [
    "project",
    "developed",
    "implemented",
    "designed",
    "created",
    "built"
]

EXPERIENCE_KEYWORDS = [
    "experience",
    "engineer",
    "developer",
    "analyst",
    "executive",
    "associate",
    "intern"
]


def classify_line(line):

    text = line.strip()

    lower = text.lower()

    if not text:
        return "EMPTY"

    if re.search(r"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}", text):
        return "EMAIL"

    if re.search(r"(?:\+91[- ]?)?[6-9]\d{9}", text):
        return "PHONE"

    if "linkedin.com" in lower:
        return "LINKEDIN"

    if "github.com" in lower:
        return "GITHUB"

    if any(word in lower for word in DEGREE_KEYWORDS):
        return "DEGREE"

    if any(word in lower for word in INSTITUTION_KEYWORDS):
        return "INSTITUTION"

    if re.search(r"(19|20)\d{2}\s*[-–]\s*(19|20)\d{2}", text):
        return "YEAR"

    if "cgpa" in lower:
        return "CGPA"

    if "%" in text:
        return "PERCENTAGE"

    if any(word in lower for word in EXPERIENCE_KEYWORDS):
        return "EXPERIENCE"

    if any(word in lower for word in PROJECT_KEYWORDS):
        return "PROJECT"

    return "TEXT"