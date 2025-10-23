import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.context_helper import save_context

client = OpenAI()

def generate_main_story(type: str, name: str, character_history: str, context: str):
    main_story_system = "You are an imaginative worldbuilder Dungeon Master for a fantasy game."
    main_story_user = f"""
                        Generate a creative and immersive introductory story about {type} called {name}, whose adventure is just beginning. 
                        {name} is {character_history} that {context}. 
                        
                        The story title must include the current arc number, which is always 'Arc 1'. 
                        Write a detailed introduction that sets the tone, atmosphere, and emotional start of the journey.
                        
                        Also, define a dungeon for this adventure:
                            - Provide a name for the dungeon
                            - Give a brief description
                            - Specify its theme and main race (for example: Kobolds, Goblins, Fiends, Constructs, Undead, Beasts, etc.)
                       """
    main_story_schema = load_json_schema("main_story_schema.json")
    response = client.responses.create(
        model="gpt-4.1",
        input=[
            {"role": "system", "content": main_story_system},
            {"role": "user", "content": main_story_user},
        ],
        text=main_story_schema,
    )
    main_story = json.loads(response.output_text)
    save_context(main_story, "context.txt")
    return main_story
