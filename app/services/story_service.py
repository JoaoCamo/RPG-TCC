import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.context_helper import save_context, load_context

client = OpenAI()


def generate_story(choice):
    context = load_context("context.txt")
    story_system = "You are an imaginative Dungeon Master."
    story_user = f"""
                    Story Context: {context}
                    Player choice: {choice}
                    
                    Prompt: 
                    Generate a story or dialogue response from a named NPC based on the previous story context. 
                    
                    Then, provide the player with **at least 5 possible choices with a maximum of 7** for what to say or do next:
                        - The choices can cover different approaches: advancing the story (game_state: 0), interacting with the dungeon (game_state: 1), being aggressive or confrontational towards the NPC, or other creative actions.
                        - It is not required for a dungeon-related choice to appear immediately; as the player continues interacting, choices should naturally lead towards the dungeon over time.
                        - The NPC may change depending on the player's choice, and future dialogues and options should reflect these changes.
                    
                    Ensure the dialogue and choices feel natural, engaging, and consistent with the story context.
                  """
    story_schema = load_json_schema("story_schema.json")
    response = client.responses.create(
        model="gpt-4.1",
        input=[
            {"role": "system", "content": story_system},
            {"role": "user", "content": story_user},
        ],
        text=story_schema,
    )
    main_story = json.loads(response.output_text)
    save_context(main_story, "context.txt")
    return main_story
