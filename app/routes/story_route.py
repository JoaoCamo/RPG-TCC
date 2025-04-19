from flask import Blueprint, request, jsonify
from app.services.story_service import generate_story

story_blueprint = Blueprint("story", __name__)


@story_blueprint.route("/", methods=["POST"])
def story():
    return
