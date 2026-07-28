import json
import os


def load_skills():
    base_dir = os.path.dirname(os.path.dirname(__file__))

    json_path = os.path.join(
        base_dir,
        "models",
        "skills.json"
    )

    with open(json_path, "r", encoding="utf-8") as file:
        return json.load(file)