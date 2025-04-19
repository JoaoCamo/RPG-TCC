from openai import OpenAI

from app.utils.main_story_util import (
    main_story_schema,
    main_story_system,
    main_story_user,
)

client = OpenAI()


def generate_main_story():
    response = client.responses.create(
        model="gpt-4.1-mini",
        input=[
            {"role": "system", "content": main_story_system},
            {"role": "user", "content": main_story_user},
        ],
        text=main_story_schema,
    )
    return response
