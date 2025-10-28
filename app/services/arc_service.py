import json
from openai import OpenAI

from app.utils.schema_util import load_json_schema
from app.utils.context_helper import save_context, summarize_context

client = OpenAI()

def generate_arc(selected_model: str, last_action: str, current_arc: int, is_ending: bool):
    summarize_context(client)
    context = summarize_context(client)
    arc_system = "You are an imaginative worldbuilder Dungeon Master for a fantasy game."
    arc_user = f"""
                You are a narrative designer continuing an ongoing fantasy campaign.

                Story context so far: {context}

                The player's last major action was: {last_action}.
                This begins Arc {current_arc}.

                If isEnding = true, treat this as the story conclusion:
                    - The title should not include the current arc number (e.g, 'Victory of the hero')
                    - The story should reach a complete and satisfying conclusion.
                    - Do not include the arc number or suggest future events.
                    - Focus on wrapping up the narrative and giving closure to the characters and world.

                If isEnding = false, generate a creative and immersive continuation for this new story arc:
                    - Include the current arc number in the title (e.g., 'Arc {current_arc} – ...').
                    - Write an engaging introduction that sets the tone, atmosphere, and direction for this stage of the adventure.

                Ensure the narrative naturally follows the established context and reflects the consequences of the player's previous actions.
                """

    arc_schema = load_json_schema("arc_schema.json")
    response = client.responses.create(
        model=selected_model,
        input=[
            {"role": "system", "content": arc_system},
            {"role": "user", "content": arc_user},
        ],
        text=arc_schema,
    )
    arc = json.loads(response.output_text)
    save_context(arc, "context.txt")
    return arc