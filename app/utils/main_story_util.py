import json
import os

main_story_system = "You are an experienced Dungeons and Dragons Dungeon Master."

main_story_user = "Generate an introductory story about a level 1 paladin called Mubrin that enters a kingdom and wants to become a hero by defeating its dungeon. Define a name and a brief description for the kingdom and for the dungeon."

# Load main_story_schema from JSON file
current_dir = os.path.dirname(__file__)
schema_path = os.path.join(current_dir, "main_story_schema.json")

with open(schema_path, "r") as f:
    main_story_schema = json.load(f)
