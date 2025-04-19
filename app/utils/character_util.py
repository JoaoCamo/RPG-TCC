import json
import os

character_system = "You are an experienced Dungeons and Dragons Dungeon Master."

character_user = "Generate a character sheet for an RPG character."

# Load character_schema from JSON file
current_dir = os.path.dirname(__file__)
schema_path = os.path.join(current_dir, "character_schema.json")

with open(schema_path, "r") as f:
    character_schema = json.load(f)
